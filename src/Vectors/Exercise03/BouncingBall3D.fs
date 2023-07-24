//! Exercise 1.03: 3D bouncing ball
module NatureOfCode.Vectors.Exercise03.BouncingBall3D

open P5.Core
open P5.Rendering
open P5.Environment
open P5.Color
open P5.Shape
open P5.Math
open P5.ThreeD
open P5.Structure
open P5.Transform

let boxWidth = 200
let sphereRadius = 20

type Ball =
    { location: P5Vector
      velocity: P5Vector }

let setup p5 =
    createWebGLCanvas p5 720 400

    { location = P5Vector.create (0, 0, 0)
      velocity = P5Vector.create (2.5, 5, 3) }

let update p5 (ball: Ball) =
    // Immutable versions are only covered later in the book.
    ball.location.addVector ball.velocity

    let boundary = (boxWidth / 2) - sphereRadius |> float

    if abs (ball.location.x) >= boundary then
        do ball.velocity.x <- -ball.velocity.x

    if abs (ball.location.y) >= boundary then
        do ball.velocity.y <- -ball.velocity.y

    if abs (ball.location.z) >= boundary then
        do ball.velocity.z <- -ball.velocity.z

    ball

let draw p5 (ball: Ball) =
    smooth p5
    background p5 (Grayscale 255)
    orbitControl p5

    // Box
    stroke p5 (Grayscale 0)
    strokeWeight p5 2
    noFill p5
    cube p5 boxWidth

    // Sphere
    fill p5 (Grayscale 175)
    push p5
    noStroke p5
    translate3D p5 ball.location.x ball.location.y ball.location.z
    sphere p5 sphereRadius
    pop p5

let run node = simulate node setup update draw
