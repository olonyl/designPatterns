// Learn more about F# at http://fsharp.org

open System

type ICountryInfo = 
    abstract member Capital: string

type Country =
    | USA
    | UK
with 
    static member Create = function
        | "USA" | "America" -> USA
        | "UK" | "England" -> UK
        | _ -> failwith "No such country"

let make country =
    match country with 
    | USA -> {new ICountryInfo with 
                member x.Capital = "Washington"}
    | UK -> {new ICountryInfo with
                member x.Capital = "London"}

[<EntryPoint>]
let main argv =
    let uk = Country.Create "UK"
    let usa = make Country.USA
    printfn "%s" usa.Capital
    0 // return an integer exit code
