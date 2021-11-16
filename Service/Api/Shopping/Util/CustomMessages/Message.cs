using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shopping.Util.CustomMessages
{
    public class Message
    {
        public Message(int statusCode = StatusCodes.Status424FailedDependency, string title = null, ModelStateDictionary modelStateDictionary = null, Exception exception = null, IDictionary<string, string> notifications = null, SimpleMessage simpleMessage = null)
        {
            this.StatusCode = statusCode;
            this.Title = title;
            this.ModelStateDictionary = modelStateDictionary;
            this.Exception = exception;
            this.Notifications = notifications;
            this.SimpleMessage = simpleMessage;
        }
        public int StatusCode { get; private set; }
        public string Title { get; private set; }
        public ModelStateDictionary ModelStateDictionary { get; private set; }
        public Exception Exception { get; private set; }
        public IDictionary<string, string> Notifications { get; private set; }
        public SimpleMessage SimpleMessage { get; private set; }
    }
}