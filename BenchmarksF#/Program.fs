module InterpolatedStringBenchmarks

open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

let world = "world"

let two = "2"

[<MemoryDiagnoser>]
type Bench() =
    [<Benchmark>]
    member _.InterpolatedStringCurrentHW() = $"hello {world}"

    [<Benchmark>]
    member _.StringFormatHW() = String.Format("hello {0}", world)

    [<Benchmark>]
    member _.StringBuilderHW() =
        let sb = Text.StringBuilder()
        sb.Append("hello") |> ignore
        sb.Append(world) |> ignore
        sb.ToString()

    [<Benchmark>]
    member _.DefaultInterpolatedStringHandlerHW() =
        let sb = System.Runtime.CompilerServices.DefaultInterpolatedStringHandler(1, 1)
        sb.AppendLiteral("hello")
        sb.AppendFormatted(world)
        sb.ToStringAndClear()

    [<Benchmark>]
    member _.InterpolatedStringCurrentTT() = $"{two} plus {2} is {4}."

    [<Benchmark>]
    member _.StringFormatTT() = String.Format("{0} plus {1} is {2}.", two, 2, 4)

    [<Benchmark>]
    member _.StringBuilderTT() =
        let sb = Text.StringBuilder()
        sb.Append(two) |> ignore
        sb.Append(" plus ") |> ignore
        sb.Append(2) |> ignore
        sb.Append(" is ") |> ignore
        sb.Append(4) |> ignore
        sb.ToString()

    [<Benchmark>]
    member _.DefaultInterpolatedStringHandlerTT() =
        let sb = System.Runtime.CompilerServices.DefaultInterpolatedStringHandler(2, 3)
        sb.AppendFormatted(two)
        sb.AppendLiteral(" plus ")
        sb.AppendFormatted(2)
        sb.AppendLiteral(" is ")
        sb.AppendFormatted(4)
        sb.ToStringAndClear()

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<Bench>() |> ignore
    0 // return an integer exit code