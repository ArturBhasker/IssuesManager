using IssuesManager.Domain.Models;
using System.Collections.Concurrent;

namespace IssuesManager.Domain
{
    public class FileStorageService : IFileStorageService
    {
        private static readonly IDictionary<string, byte[]?> _storage = new ConcurrentDictionary<string, byte[]?>();

        public async Task AddFile(Stream contentStream, string name, CancellationToken cancellationToken)
        {
            var byteArray = new byte[contentStream.Length];

            await contentStream.ReadAsync(byteArray);

            _storage.Add(name, byteArray);
        }

        public void RemoveFiles(string[] storageIds)
        {
            foreach(var storageId in storageIds)
            {
                _storage.Remove(storageId);
            }
        }

        public Stream? ReceiveFile(string storageId)
        {
            var content = _storage
                .FirstOrDefault(s => s.Key == storageId)
                .Value;

            if(content is not null)
            {
                return new MemoryStream(content);
            }

            return null;
        }
    }
}
