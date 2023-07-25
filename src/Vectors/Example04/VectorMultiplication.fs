//! Example 1.04: Multiplying a vector
module NatureOfCode.Vectors.Example04.VectorMultiplication

open P5.Core
open P5.Rendering
open P5.Color
open P5.Math
open P5.Events
open P5.Environment
open P5.Transform
open P5.Shape

let setup p5 = createCanvas p5 720 400

let draw p5 _ =
    strokeWeight p5 2
    let centerX = width p5 / 2 |> float
    let centerY = height p5 / 2 |> float

    background p5 (Grayscale 255)
    let mouse = P5Vector.create (mouseX p5, mouseY p5)
    let center = P5Vector.create (centerX, centerY)
    mouse.subVector center

    mouse.multScalar 0.5

    translate p5 centerX centerY
    line p5 0 0 mouse.x mouse.y

let run node = animate node setup draw
