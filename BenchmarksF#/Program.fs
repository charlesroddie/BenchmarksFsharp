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
    member _.StringFormat() = String.Format("hello {0}", world)

    [<Benchmark>]
    member _.StringBuilder() =
        let sb = Text.StringBuilder()
        sb.Append("hello") |> ignore
        sb.Append(world) |> ignore
        sb.ToString()

    [<Benchmark>]
    member _.DefaultInterpolatedStringHandler() =
        let sb = System.Runtime.CompilerServices.DefaultInterpolatedStringHandler(1, 1)
        sb.AppendLiteral("hello")
        sb.AppendFormatted(world)
        sb.ToStringAndClear()

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<Bench>() |> ignore
    0 // return an integer exit code