module FSharpBenchmarks

open System.Collections.Immutable
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

[<MemoryDiagnoser>]
type Bench() =
    

    [<Benchmark>]
    member _.Benchmark() = ()

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<Bench>() |> ignore
    0 // return an integer exit code