using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Dokumenter.Domain.Factories
{
    /// <summary>
    /// Bindings kræver credentials og er nemmmere at have
    /// i kode end i konfiguration.
    /// http://www.rauch.io/2015/06/25/all-wcf-timeouts-explained/
    /// </summary>
    public class BindingFactory
    {
        private const long MaxSize = 268435456;
        private const int waitMinutes = 10;
        private const int openMinutes = 10;


        public BasicHttpBinding CreateBasicHttpBinding(string name)
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            binding.MaxBufferPoolSize = MaxSize;
            binding.MaxReceivedMessageSize = MaxSize;
            binding.MaxBufferSize = (int)MaxSize;

            binding.SendTimeout = TimeSpan.FromMinutes(20);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(20);

            binding.Name = name;

            binding.ReaderQuotas.MaxDepth = (int)MaxSize;
            binding.ReaderQuotas.MaxStringContentLength = (int)MaxSize;
            binding.ReaderQuotas.MaxArrayLength = (int)MaxSize;
            binding.ReaderQuotas.MaxBytesPerRead = (int)MaxSize;
            binding.ReaderQuotas.MaxNameTableCharCount = (int)MaxSize;

            return binding;
        }

        /// <summary>
        /// Binding med credentials.
        /// </summary>
        /// <returns></returns>
        public WSHttpBinding CreateWSHttpBinding(string name)
        {
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.None);

            binding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.MaxBufferPoolSize = MaxSize;
            binding.MaxReceivedMessageSize = MaxSize;
            binding.ReceiveTimeout = new TimeSpan(0, waitMinutes, 0);
            binding.OpenTimeout = new TimeSpan(0, openMinutes, 0);
            binding.Name = name;

            return binding;
        }
    }
}
