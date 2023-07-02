module NatureOfCode.App

open P5.Core
open NatureOfCode

let private sketches =
    [ "Introduction/Example01/TraditionalRandomWalk", Introduction.Example01.TraditionalRandomWalk.run
      "Introduction/Example02/RandomNumberDistribution", Introduction.Example02.RandomNumberDistribution.run
      "Introduction/Example03/RightWalker", Introduction.Example03.RightWalker.run
      "Introduction/Exercise01/DownRightWalker", Introduction.Exercise01.DownRightWalker.run
      "Introduction/Exercise03/DynamicRandomWalker", Introduction.Exercise03.DynamicRandomWalker.run ]
    |> Map.ofList

let runSketch name canvasSelector =
    let node = Element <| Browser.Dom.document.querySelector (canvasSelector)

    if not <| sketches.ContainsKey name then
        failwith <| sprintf "No sketch with name %s exists" name

    sketches[name]node

let allNames = sketches |> Map.toSeq |> Seq.map fst |> Seq.toArray
