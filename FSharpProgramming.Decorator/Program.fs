// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics

let doWork() = 
    printfn "Doing some work"

let logger work name = 
    let sw = Stopwatch.StartNew()
    printfn "%s %s" "Entering function" name
    work()
    sw.Stop()
    printfn "Exeting method %s: %f elapsed" name sw.Elapsed.TotalSeconds
    
[<EntryPoint>]
let main argv =
    let work() = logger doWork "do_work"
    work()
    0 // return an integer exit code
