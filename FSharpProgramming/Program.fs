// Learn more about F# at http://fsharp.org

open System

let p args = 
    let allArgs = args |> String.concat "\n"
    ["<p>"; allArgs; "</p>"] |> String.concat "\n"

let img url = "<img src=\"" +  url + "\"/>"

[<EntryPoint>]
let main argv =
    let html =
        p [
            "Check out this picture"
            img "pokemon.com/picachu.png"
        ]
    printfn "%s" html
    0
