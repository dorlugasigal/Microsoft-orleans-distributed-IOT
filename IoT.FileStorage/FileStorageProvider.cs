using Newtonsoft.Json;
using Orleans;
using Orleans.Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IoT.FileStorage
{
    public class FileStorageProvider : IStorageProvider
    {
        public Task ClearStateAsync(string grainType, Orleans.Runtime.GrainReference grainReference, IGrainState grainState)
        {
            var fileInfo = GetFileInfo(grainType, grainReference);
            fileInfo.Delete();
            return TaskDone.Done;
        }

        public Task Close()
        {
            return TaskDone.Done;
        }

        public Orleans.Runtime.OrleansLogger Log
        {
            get;
            set;
        }

        public async Task ReadStateAsync(string grainType, Orleans.Runtime.GrainReference grainReference, Orleans.IGrainState grainState)
        {
            var fileInfo = GetFileInfo(grainType, grainReference);
            if (!fileInfo.Exists) return;

            using (var stream = fileInfo.OpenText())
            {
                var json = await stream.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                grainState.SetAll(data);
            }
        }

        public Task WriteStateAsync(string grainType, Orleans.Runtime.GrainReference grainReference, Orleans.IGrainState grainState)
        {
            var json = JsonConvert.SerializeObject(grainState.AsDictionary());
            var fileInfo = GetFileInfo(grainType, grainReference);
            using (var stream = fileInfo.OpenWrite())
            using (var writer = new StreamWriter(stream))
            {
                return writer.WriteAsync(json);
            }
        }

        FileInfo GetFileInfo(string grainType, Orleans.Runtime.GrainReference grainReference)
        {
            return new FileInfo(Path.Combine(directory, string.Format("{0}-{1}.json", grainType, grainReference.ToKeyString())));
        }

        public Task Init(string name, Orleans.Providers.IProviderRuntime providerRuntime, Orleans.Providers.IProviderConfiguration config)
        {
            this.Name = name;
            directory = config.Properties["directory"];
            return TaskDone.Done;
        }

        string directory;
        public string Name { get; set; }
    }
}
