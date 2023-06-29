module NatureOfCode.App

open P5.Core
open NatureOfCode

let runSketch name canvasSelector =
    let node = Element <| Browser.Dom.document.querySelector (canvasSelector)

    match name with
    | "Introduction/Example01" -> Introduction.Example01.TraditionalRandomWalk.run node
    | _ -> failwith <| sprintf "No sketch with name %s exists" name
