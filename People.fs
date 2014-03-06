module People

open System
open System.IO
open System.Text
open System.Runtime.Serialization.Json
open System.Xml

type Name = string
type Location = string
type Person = {name:Name; loc:Location}


// Object to Json 
let public json<'t> (myObj:'t) =   
    use ms = new MemoryStream() 
    (new DataContractJsonSerializer(typeof<'t>)).WriteObject(ms, myObj) 
    Encoding.Default.GetString(ms.ToArray()) 