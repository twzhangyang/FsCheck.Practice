namespace FsCheck.XUnit.QuickStart.FSharp

module DollarModule =
    open FsCheck.Xunit

    type Dollar(amount : int) =
        let mutable privateAmount = amount;

        member this.Amount = privateAmount
        member this.Add add =
            privateAmount <- this.Amount + add
        member this.Times multiplier =
            privateAmount <- this.Amount * multiplier
        static member Create amount =
            Dollar amount

    [<Property>]
    let ``set then get should give same result`` value =
        let obj = Dollar.Create 0
        obj.Add value
        let newValue = obj.Amount
        value = newValue
        
    [<Property>]
    let ``add then multiplier same as create`` value times =
        let dollar = Dollar.Create 0
        dollar.Add value
        dollar.Times times
        
        let dollar2 = Dollar.Create(value*times);
        
        dollar.Amount = dollar2.Amount
