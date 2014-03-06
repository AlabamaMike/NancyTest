
// NOTE: If warnings appear, you may need to retarget this project to .NET 4.0. Show the Solution
// Pad, right-click on the project node, choose 'Options --> Build --> General' and change the target
// framework to .NET 4.0 or .NET 4.5.

module WebServers
 
open System
open System.IO

open Nancy
open Nancy.Hosting.Self
open Nancy.Conventions
open People

let (?) (this : obj) (prop : string) : obj =
    (this :?> DynamicDictionary).[prop]
 
let siteRoot = @"/Users/michael/content"
 
type WebServerModule() as this =
    inherit NancyModule()

    let paul:Person = {name = "Paul Blair";loc = "NYC"}
    let denomy:Person = {name = "Michael Denomy";loc = "BOS"}

    do this.Get.["{file}"] <-
         fun parameters ->
              new Nancy.Responses.HtmlResponse(
                  HttpStatusCode.OK,
                  (fun (s:Stream) ->
                      let file = (parameters?file).ToString()
                      printfn "Requested : '%s'" file
                      let bytes = File.ReadAllBytes(Path.Combine(siteRoot, file))
                      s.Write(bytes,0,bytes.Length)
              )) |> box
 
    do this.Get.["/users"] <-
        fun param ->
            new Nancy.Responses.HtmlResponse(
                HttpStatusCode.OK,
                (fun (s:Stream) ->
                    let responseString = People.json [paul;denomy]
                    let responseText = System.Text.Encoding.ASCII.GetBytes(responseString)
                    s.Write(responseText,0,responseText.Length)
                )
            ) |> box


let startAt host =
    let nancyHost = new NancyHost(new Uri(host))
    nancyHost.Start()
    nancyHost
 
let server = startAt "http://localhost:8080/"
printfn "Press [Enter] to exit."
Console.ReadKey() |> ignore
server.Stop()
