using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibraryAuction
{
    public class ErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            Directory.CreateDirectory(".\\Errors\\");
            string path = ".\\Errors\\" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".txt";
            using (StreamWriter writer = new StreamWriter(path, true))
                writer.WriteLine(error);
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var newEx = new FaultException(
                 string.Format("Exception caught at Service Application " +
         "    GlobalErrorHandler {0} Method: {1 }{2} Message: {3} ",
                Environment.NewLine, error.TargetSite.Name, 
                Environment.NewLine, error.Message));

            MessageFault msgFault = newEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }
    }
}
