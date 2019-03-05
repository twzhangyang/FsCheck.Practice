namespace FsCheck.ModelBased.FSharp.Testing

module CounterTests =
    open FsCheck

    type Counter() =
        let mutable n = 0
        member __.Inc() = n <- n + 1
        member __.Dec() = if n > 2 then n <- n - 2 else n <- n - 1
        member __.Get() = n
        member __.Reset() = n <- 0
        override __.ToString() = sprintf "Counter=%i" n

    let spec =
        let inc = { new Command<Counter, int>() with
                        override __.RunActual counter = counter.Inc(); counter
                        override __.RunModel m = m + 1
                        override __.Post(counter, m) = counter.Get() = m |@ sprintf "model: %i <> %A" m counter
                        override __.ToString() = "inc" }

        let dec = { new Command<Counter, int>() with
                        override __.RunActual counter = counter.Dec(); counter
                        override __.RunModel m = m - 1
                        override __.Post(counter, m) = counter.Get() = m |@ sprintf "model: %i <> %A" m counter
                        override __.ToString() = "dec" }

        { new ICommandGenerator<Counter, int> with
            member __.InitialActual = Counter()
            member __.InitialModel = 0
            member __.Next model = Gen.elements [ inc; dec ] }


    [<Xunit.Fact>]
    let ``test counter spec`` =
        Check.Quick (Command.toProperty spec)
        
    