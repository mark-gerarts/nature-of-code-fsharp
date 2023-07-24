//! Example 1.02: Vector walk
module NatureOfCode.Vectors.Exercise02.VectorWalk

open P5.Core
open P5.Environment
open P5.Color
open P5.Shape
open P5.Rendering
open P5.Math

type Walker = P5Vector

let setup p5 =
    createCanvas p5 720 400
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)

    P5Vector.create (width p5 / 2 |> float, height p5 / 2 |> float)

let update p5 (walker: Walker) =
    let choices = [| -1; 0; 1 |]

    let xDiff = randomChoice p5 choices
    let yDiff = randomChoice p5 choices

    walker.add (xDiff, yDiff)
    walker

let draw p5 (walker: Walker) =
    // Adding .5 yields better results with regards to aliasing.
    point p5 (float walker.x + 0.5) (float walker.y + 0.5)

let run node = simulate node setup update draw
