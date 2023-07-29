//! Example 1.07: Motion 101 (velocity)
module NatureOfCode.Vectors.Example07.Motion101Velocity

open P5.Core
open P5.Rendering
open P5.Color
open P5.Math
open P5.Environment
open P5.Shape

type Mover =
    { location: P5Vector
      velocity: P5Vector }

let createMover p5 =
    let width = width p5 |> float
    let height = height p5 |> float

    let location = P5Vector.create (randomMax p5 width, randomMax p5 height)
    let velocity = P5Vector.create (randomBetween p5 -3 3, randomBetween p5 -3 3)

    { location = location
      velocity = velocity }

let updateMover p5 mover =
    mover.location.addVector mover.velocity

    let width = width p5 |> float
    let height = height p5 |> float

    mover.location.x <-
        match mover.location.x with
        | x when x > width -> 0
        | x when x < 0 -> width
        | x -> x

    mover.location.y <-
        match mover.location.y with
        | y when y > height -> 0
        | y when y < 0 -> height
        | y -> y

    mover

let displayMover p5 mover =
    strokeWeight p5 2
    stroke p5 (Grayscale 0)
    fill p5 (Grayscale 175)
    circle p5 mover.location.x mover.location.y 48

let setup p5 =
    createCanvas p5 720 400
    createMover p5

let update = updateMover

let draw p5 mover =
    background p5 (Grayscale 255)
    displayMover p5 mover

let run node = simulate node setup update draw
