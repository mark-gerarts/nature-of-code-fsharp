//! Exercise 1.06: Perlin noise acceleration
module NatureOfCode.Vectors.Exercise06.PerlinNoiseAcceleration

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
    let acceleration = P5Vector.create ()
    let t = (millis p5 |> float) / 1000.0
    let xOff = t
    let yOff = t + 1000.0
    let accX = map p5 (noise p5 xOff) 0 1 -1 1
    let accY = map p5 (noise p5 yOff) 0 1 -1 1
    acceleration.set (accX, accY)

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
