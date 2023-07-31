//! Example 1.09: Motion 101 (velocity and random acceleration)
module NatureOfCode.Vectors.Example09.Motion101RandomAcceleration

open P5.Core
open P5.Rendering
open P5.Color
open P5.Math
open P5.Environment
open P5.Shape

type Mover =
    { location: P5Vector
      velocity: P5Vector
      topSpeed: float }

let createMover p5 =
    let width = width p5 |> float
    let height = height p5 |> float

    let location = P5Vector.create (randomMax p5 width, randomMax p5 height)
    let velocity = P5Vector.create (0, 0)

    { location = location
      velocity = velocity
      topSpeed = 10 }

let updateMover p5 mover =
    let acceleration = P5Vector.random2D ()
    acceleration.multScalar (randomMax p5 2.0)

    mover.velocity.addVector acceleration
    mover.velocity.limit mover.topSpeed
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
