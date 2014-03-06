module People

open System
open Newtonsoft.Json

type Name = string
type Location = string
type Person = {name:Name; loc:Location}

let public json<'t> (myObj:'t) =   
    Newtonsoft.Json.JsonConvert.SerializeObject myObj

(* // Object to Json 
let public json<'t> (myObj:'t) =   
    use ms = new MemoryStream() 
    (new DataContractJsonSerializer(typeof<'t>)).WriteObject(ms, myObj) 
    Encoding.Default.GetString(ms.ToArray()) *) 