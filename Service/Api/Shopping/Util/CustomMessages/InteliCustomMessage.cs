using InteliSystem.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Shopping.Resources;

namespace Shopping.Util.CustomMessages
{
    public static class InteliCustomMessage
    {
        public static ActionResult MountMessage(Message message, IStringLocalizer<Messages> localizer)
        {
            var statusCode = message.StatusCode;
            var lerros = new List<object>();

            if (message.ModelStateDictionary != null)
            {
                foreach (var key in message.ModelStateDictionary.Keys)
                {
                    var erro = message.ModelStateDictionary[key];
                    var mensagens = new List<string>();
                    foreach (var msg in erro.Errors)
                    {
                        mensagens.Add(msg.ErrorMessage);
                    }
                    if (mensagens.Count <= 0)
                    {
                        continue;
                    }
                    dynamic objerro = new
                    {
                        key,
                        mensagens
                    };
                    lerros.Add(objerro);
                }

                if (statusCode == StatusCodes.Status200OK)
                    statusCode = StatusCodes.Status406NotAcceptable;

            }

            if (message.Notifications != null && message.Notifications.Count > 0)
            {
                var chaveX = "";
                var chave = "";
                IList<string> mensagens = null;
                message.Notifications.OrderBy(c => c.Key).ToList().ForEach(erro =>
                {
                    chave = erro.Key;

                    if (chaveX.IsEmpty())
                    {
                        chaveX = chave;
                        mensagens = new List<string>();
                    }


                    if (chaveX != chave)
                    {
                        lerros.Add(new
                        {
                            key = chaveX,
                            mensagens
                        });
                        mensagens = new List<string>();
                        chaveX = chave;
                        mensagens.Add(localizer[erro.Value].Value);
                    }
                    else
                    {
                        mensagens.Add(localizer[erro.Value].Value);
                    }
                });

                lerros.Add(new
                {
                    key = chave,
                    mensagens = mensagens
                });
            }

            if (message.Exception != null)
            {
                var messageInner = "";
                if (message.Exception.InnerException != null)
                    messageInner = $" - Inner Exception: {message.Exception.InnerException.Message}";
                lerros.Add(new
                {
                    key = "Error",
                    mensagens = $"{message.Exception.Message}{messageInner}"
                });
                statusCode = StatusCodes.Status500InternalServerError;
            }

            if (message.SimpleMessage != null)
            {
                lerros.Add(new
                {
                    Key = message.SimpleMessage.Key,
                    messageInner = message.SimpleMessage.Message
                });
            }
            dynamic retorno = new
            {
                status = statusCode,
                titulo = message.Title,
                erros = lerros
            };
            var objeresult = new ObjectResult(retorno);
            objeresult.StatusCode = statusCode;
            return objeresult;
        }
    }
}