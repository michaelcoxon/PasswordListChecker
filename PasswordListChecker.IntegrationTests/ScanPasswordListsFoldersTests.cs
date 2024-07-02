using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bloomy.Lib.Filter;
using Xunit;

namespace PasswordListChecker.IntegrationTests
{
    public class ScanPasswordListsFoldersTests : IAsyncLifetime
    {
        private PasswordList _passwordList;

        public async Task InitializeAsync()
        {
            var folder = "J:\\SecLists\\Passwords";

            var deserializer = new NewLineSeparatedPasswordListDeserializer();
            var builder = new PasswordListBuilder();

            var files = Directory.EnumerateFiles(folder, "*.txt", SearchOption.AllDirectories).ToList();

            Console.WriteLine($"[{DateTime.Now}] found {files.Count} files");

            var disposables = new List<IDisposable>();
            try
            {
                foreach (var file in files)
                {
                    var stream = File.Open(file, FileMode.Open, FileAccess.Read);
                    disposables.Add(stream);
                    var source = new StreamPasswordListSource(stream, deserializer);
                    builder.AddSource(source);
                }

                Console.WriteLine($"[{DateTime.Now}] sources added");

                this._passwordList = await builder.BuildAsync();

                Console.WriteLine($"[{DateTime.Now}] builder built");


            }
            finally
            {
                foreach (var dis in disposables)
                {
                    dis.Dispose();
                }
            }

        }

        [Fact]
        public async Task Test1()
        {
            Assert.Contains("nsroot", this._passwordList);
        }

        //[Fact]
        //public async Task Test2()
        //{
        //    var filter = new BasicFilter(this._passwordList.Count, HashFunc.Murmur3);

        //    foreach (var item in this._passwordList.Where(i => !string.IsNullOrEmpty(i)))
        //    {
        //        try
        //        {
        //            filter.Insert(item);
        //        }
        //        catch (Exception ex)
        //        {
        //            _ = ex;
        //            throw;
        //        }
        //    }
        //    var res = filter.Check("nsroot");

        //    Assert.Equal(BloomPresence.MightBeInserted, res.Presence);
        //    Assert.Contains("quiksilver", this._passwordList);
        //}

        Task IAsyncLifetime.DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
