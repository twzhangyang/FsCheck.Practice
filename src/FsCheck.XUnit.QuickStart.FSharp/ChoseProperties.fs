namespace FsCheck.QuickStart.FSharp

open FsCheck.Xunit
open System

module DifferentPathsSameDestination =

    let badSortFn aList:int list = aList
   
    let sortFn = List.sort
    // let sortFn = badSortFn
    
    [<Property(Verbose=true)>]
    let ``+1 then sort should be same as sort then +1`` aList =
        let add1 x = x + 1
        
        let result1 = aList |> sortFn |> List.map add1
        let result2 = aList |> List.map add1 |> sortFn
        
        result1 = result2

    [<Property(Verbose=true)>]
    let ``append minValue then sort should be same as sort then prepend minValue`` aList =
        let minValue = Int32.MinValue
        
        let appendThenSort = (aList @[minValue]) |> sortFn
        let sortThenPrepend = minValue :: (aList |> sortFn)
        
        appendThenSort = sortThenPrepend
        
    [<Property>]
    let ``negate then sort should be same as sort than negate then reverse`` aList =
        let negate x = x * -1
        
        let negateThenSort = aList |> List.map negate |> List.sort
        let sortThenNegateThenReverse = aList |> List.sort |> List.map negate |> List.rev
        
        negateThenSort = sortThenNegateThenReverse
        
    [<Property>]
    let ``append any value then reverse should be same as reverse then prepend same value`` anyValue aList =
        let appendThenReverse = (aList @ [anyValue]) |> List.rev
        let reverseThenPrepend = anyValue :: (aList |> List.rev)
        
        appendThenReverse = reverseThenPrepend
        
module ThereAndBackAgain =
    
    [<Property>]
    let ``reverse then reverse should be same as original`` (aList:int list)=
        let reverseThenReverse = aList |> List.rev |> List.rev
        reverseThenReverse = aList

module HardToProveEasyToVerify =
    
    [<Property>]
    let ``concatting the elements of a string split by commas recreates the original string`` aListOfStrings =
        
        let addWithComma s t = s + "." + t
        let originalString = aListOfStrings |> List.fold addWithComma ""
        
        let tokens = originalString.Split [|','|]
        let recombinedString = tokens |> Array.fold addWithComma ""
        
        originalString = recombinedString
        
    [<Property>]    
    let ``adjacent paris from a list should be ordered`` (aList: int list) =
        let paris = aList |> List.sort |> Seq.pairwise
        paris |> Seq.forall (fun (x,y) -> x <=y )
        
    [<Property>]
    let ``list is sorted`` (aList: int list) =
        let prop1 = ``adjacent paris from a list should be ordered``
        let prop2 = ``concatting the elements of a string split by commas recreates the original string``
        
        true
//        prop1 .&. prop2

    [<Property>]
    let ``list is sorted 2`` (aList: int list) =
//        let prop1 = ``adjacent paris from a list should be ordered`` |@ "adjacent paris from a list should be ordered"
//        let prop2 = ``concatting the elements of a string split by commas recreates the original string`` |@ "a sorted list has some contents as the original list"

        true
//        prop1 .&. prop2

module SomethingNeverChange =
    let ``sort should have same length as original`` (aList:int list) =
        let sorted = aList |> List.sort 
        List.length sorted = List.length aList

    