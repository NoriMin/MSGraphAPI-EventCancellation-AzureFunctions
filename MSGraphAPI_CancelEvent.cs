#r "Newtonsoft.json"

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    string resourceId = "https://graph.microsoft.com";
    string tenantId = "[Directory ID displayed on property of AAD window]";
    string authString = "https://login.microsoftonline.com/" + tenantId;

    //AAD
    string clientId = "[Application ID displayed on AAD window]";
    string clientSecret = "[AAD Secret Key]";

    var authenticationContext = new AuthenticationContext(authString, false);

    ClientCredential clientCred = new ClientCredential(clientId, clientSecret);
    AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(resourceId, clientCred);
    string token = authenticationResult.AccessToken;
    log.Verbose("token=" + token);

    var responseString = String.Empty;

    //Cancellation of the event
    responseString = CancelEvent(token);

    return req.CreateErrorResponse(HttpStatusCode.OK, responseString);

}

private static string CancelEvent(string accessToken)
{
    using (var client = new HttpClient())
    {
        string graphService = "https://graph.microsoft.com/beta/users/[user_id]/events/[event_id]/cancel";

        //Headers & Methods
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, graphService);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //JSON request body
        var comment = new MessageRequest()
        {
            Comment = "This event was cancelled because noone in the room for 15 min."
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");

        using (var response = client.SendAsync(request).Result)
        {
            if(response.IsSuccessStatusCode)
            {
                return "Completed send message";
            }
            
            return response.ReasonPhrase;
        }
    }
}

class MessageRequest
{
    public string Comment { get; set; }
}   