module InterpolatedStringBenchmarks

open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

let world = "world"

[<MemoryDiagnoser>]
type Bench() =
    [<Benchmark>]
    member _.InterpolatedStringCurrent() = $"hello {world}"

    [<Benchmark>]
    member _.Impl1() = String.Format("hello {0}", world)

    [<Benchmark>]
    member _.Impl2() =
        let sb = Text.StringBuilder()
        sb.Append("hello") |> ignore
        sb.Append(world) |> ignore
        sb.ToString()

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<Bench>() |> ignore
    0 // return an integer exit code