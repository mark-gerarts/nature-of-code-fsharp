//! Example 1.02: Bouncing ball with PVectors
module NatureOfCode.Vectors.Example02.BouncingBallWithPVectors

open P5.Core
open P5.Rendering
open P5.Environment
open P5.Color
open P5.Shape
open P5.Math

type Ball =
    { location: P5Vector
      velocity: P5Vector }

let setup p5 =
    createCanvas p5 720 400

    { location = P5Vector.create (100, 100)
      velocity = P5Vector.create (2.5, 5) }

let update p5 (ball: Ball) =
    let width = width p5 |> float
    let height = height p5 |> float

    // Immutable versions are only covered later in the book.
    ball.location.addVector ball.velocity

    if ball.location.x >= width || ball.location.x <= 0 then
        do ball.velocity.x <- -ball.velocity.x

    if ball.location.y >= height || ball.location.y <= 0 then
        do ball.velocity.y <- -ball.velocity.y

    ball

let draw p5 (ball: Ball) =
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)
    strokeWeight p5 2
    fill p5 (Grayscale 175)
    circle p5 ball.location.x ball.location.y 48

let run node = simulate node setup update draw
