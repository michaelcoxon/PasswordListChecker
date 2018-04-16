using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordListChecker
{
    public static class Extensions
    {
        public static PasswordListBuilder AddUriSource(this PasswordListBuilder builder, Uri uri, IPasswordListDeserializer passwordListDeserializer = null)
        {
            passwordListDeserializer = passwordListDeserializer ?? new NewLineSeparatedPasswordListDeserializer();

            var source = new HttpPasswordListSource(uri, passwordListDeserializer);
            builder.AddSource(source);

            return builder;
        }

        public static PasswordListBuilder AddUriSource(this PasswordListBuilder builder, string uri, IPasswordListDeserializer passwordListDeserializer = null)
        {
            passwordListDeserializer = passwordListDeserializer ?? new NewLineSeparatedPasswordListDeserializer();

            var source = new HttpPasswordListSource(uri, passwordListDeserializer);
            builder.AddSource(source);

            return builder;
        }
    }
}
