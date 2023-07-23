//! Example 1.01: Bouncing ball with no vectors
module NatureOfCode.Vectors.Example01.BouncingBallWithNoVectors

open P5.Core
open P5.Rendering
open P5.Environment
open P5.Color
open P5.Shape

type Ball =
    { x: float
      y: float
      xSpeed: float
      ySpeed: float }

let setup p5 =
    createCanvas p5 720 400

    { x = 100
      y = 100
      xSpeed = 1
      ySpeed = 3.3 }

let update p5 (ball: Ball) =
    let width = width p5 |> float
    let height = height p5 |> float

    let x = ball.x + ball.xSpeed
    let y = ball.y + ball.ySpeed

    let xSpeed = if x >= width || x <= 0 then -ball.xSpeed else ball.xSpeed
    let ySpeed = if y >= height || y <= 0 then -ball.ySpeed else ball.ySpeed

    { x = x
      y = y
      xSpeed = xSpeed
      ySpeed = ySpeed }

let draw p5 (ball: Ball) =
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)
    strokeWeight p5 2
    fill p5 (Grayscale 175)
    circle p5 ball.x ball.y 48

let run node = simulate node setup update draw
