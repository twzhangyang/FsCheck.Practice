namespace FsCheck.QuickStart.FSharp
open FsCheck

module Generators =
    open System

    //1. Gen.constant
    let value1 = Gen.constant ("Foo") |> Gen.sample 0 5

    //2. Gen.choose
    let value2 = Gen.choose (1, 10) |> Gen.sample 0 5

    //3. Gen.elements
    Gen.elements [ 42; 1337; 7; -100; 1453; -273 ] |> Gen.sample 0 10

    //4. Gen.growingElements
    Gen.growingElements [ 'a'; 'b'; 'c'; 'd'; 'e'; 'f'; 'g'; 'h'; 'i'; 'j' ] |> Gen.sample 3 10

    //5. Map
    Gen.choose (0, 127) |> Gen.map byte |> Gen.sample 0 10

    Gen.choose (1, 3) |> Gen.map (fun i -> DateTime(2019, 11, i).ToString "u") |> Gen.sample 0 10

    //6. ListOf
    Gen.constant 42 |> Gen.listOf |> Gen.sample 1 10

    //7. NonEmptyListOf
    Gen.elements ["foo"; "bar"; "baz"] |> Gen.nonEmptyListOf |> Gen.sample 3 4
    
    //8. Filter
    Gen.choose (1, 100)
        |> Gen.two
        |> Gen.filter (fun (x, y) -> x <> y)
        |> Gen.map (fun (x, y) -> [x; y])
        |> Gen.sample 0 10
    
    
module GenerationExample =

    //1. Gen.Constant
    let constantGen = Gen.constant ("Foo")


    // get the generator for ints
    let intGenerator = Arb.generate<int>

    // generate three ints with a maximum size of 1
    let sample1 = Gen.sample 1 3 intGenerator

    // generate three ints with a maximum size of 10
    let sample2 = Gen.sample 10 3 intGenerator

    // see how the value are clustered around the center point
    intGenerator
    |> Gen.sample 10 1000
    |> Seq.groupBy id
    |> Seq.map (fun (k, v) -> (k, Seq.length v))
    |> Seq.sortBy (fun (k, v) -> k)
    |> Seq.toList

    // tuple generator
    let tupleGenerator = Arb.generate<int * int * int>

    // generate 3 tuples with a maximum size of 1
    let ``tuple sample1`` = Gen.sample 1 3 tupleGenerator

    // generate 3 tuples with a maximum size of 10
    let ``tuple sample2`` = Gen.sample 10 3 tupleGenerator

    // int list generator
    let intListGenerator = Arb.generate<int list>

    // generate 10 int lists with a maximum size of 5
    let ``int list sample1`` = Gen.sample 5 10 intListGenerator

    // string generator
    let stringGenerator = Arb.generate<string>

    // generate 3 srings with a maximum size of 1
    let ``string sample1`` = Gen.sample 1 3 stringGenerator

    // the generator will work with user defined types
    type Color = | Red | Green of int | Blue of bool
    let colorGenrator = Arb.generate<Color>

    // generate 10 colors with a maximum size of 50
    let ``color sample1`` = Gen.sample 50 10 colorGenrator

    type Point = { x : int; y : int; color : Color }
    let pointGenerator = Arb.generate<Point>
    let ``point sample1`` = Gen.sample 50 10 pointGenerator

module ShrinkExamples =
    let shrink1 = Arb.shrink 100 |> Seq.toList
    // [0;50;75;88;94;97;99]

    let shrink2 = Arb.shrink (1, 2, 3) |> Seq.toList
    // [(0,2,3);(1,0,3);(1,1,3);(1,2,0);(1,2,2)]

    let shrink3 = Arb.shrink "abcd" |> Seq.toList
    //  ["bcd"; "acd"; "abd"; "abc"; "abca"; "abcb"; "abcc"; "abad"; "abbd"; "aacd"]

    let shrink4 = Arb.shrink [ 1; 2; 3 ] |> Seq.toList
    //  [[2; 3]; [1; 3]; [1; 2]; [1; 2; 0]; [1; 2; 2]; [1; 0; 3]; [1; 1; 3]; [0; 2; 3]]

    // silly property to test
    let isSmallerThan80 x = x < 80

    isSmallerThan80 100 // false, so start shrinking

    isSmallerThan80 0 // true
    isSmallerThan80 50 // true
    isSmallerThan80 75 // true
    isSmallerThan80 88 // false, so shrink again

    Arb.shrink 88 |> Seq.toList
    //  [0; 44; 66; 77; 83; 86; 87]
    isSmallerThan80 0 // true
    isSmallerThan80 44 // true
    isSmallerThan80 66 // true
    isSmallerThan80 77 // true
    isSmallerThan80 83 // false, so shrink again

    Arb.shrink 83 |> Seq.toList
    //  [0; 42; 63; 73; 78; 81; 82]
    // smallest failure is 81, so shrink again

    Arb.shrink 81 |> Seq.toList
    //  [0; 41; 61; 71; 76; 79; 80]
    // smallest failure is 80
    // no smaller value found
