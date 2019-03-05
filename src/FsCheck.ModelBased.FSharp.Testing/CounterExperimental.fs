namespace FsCheck.ModelBased.FSharp.Testing

module CounterExperimental=
    open FsCheck
    open FsCheck.Experimental

    type Counter(?inital:int) =
        let mutable n = defaultArg inital 0
        member __.Inc() =
            if n <= 3 then n <- n + 1 else n <- n + 2
            n
        member __.Dec() = if n <= 0 then failwith "Precondition fail" else n <- n - 1; n
        member __.Reset() = n <- 0
        override __.ToString() = sprintf "Counter = %i" n
        
    let spec =
        let inc =
            { new Operation<Counter, int>() with
                member __.Run m = m + 1
                member __.Check (counter, m) =
                    let res = counter.Inc()
                    m = res
                    |@ sprintf "Inc: model = `%i, actual = %i" m res
                override __.ToString() = "inc"}
            
            
        let dec =
            { new Operation<Counter, int>() with
                member __.Run m = m - 1
                member __.Pre m =
                    m > 2
                member __.Check (counter, m) =
                    let res = counter.Dec()
                    m = res
                    |@ sprintf "Dec: model = %i, actual = %i" m res
                override __.ToString() = "dec"}
            
        let create initalValue =
            { new Setup<Counter, int>() with
                member __.Actual() = new Counter(initalValue)
                member __.Model() = initalValue }
        
        { new Machine<Counter,int>() with
            member __.Setup = Gen.choose(0,3) |> Gen.map create |> Arb.fromGen
            member __.Next(m) = Gen.elements [inc; dec] }
            


    Check.Quick ( StateMachine.toProperty spec)