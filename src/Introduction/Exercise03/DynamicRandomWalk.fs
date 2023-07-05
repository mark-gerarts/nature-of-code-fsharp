//! Exercise I.3: Dynamic random walk
module NatureOfCode.Introduction.Exercise03.DynamicRandomWalk

open P5.Core
open P5.Environment
open P5.Color
open P5.Shape
open P5.Rendering
open P5.Math
open P5.Events

type Walker = { x: int; y: int }

let random = new System.Random()

let setup p5 =
    createCanvas p5 720 400
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)

    { x = width p5 / 2; y = height p5 / 2 }

let randomStep p5 walker =
    let choices = [| -1; 0; 1 |]
    let choiceX = randomChoice p5 choices
    let choiceY = randomChoice p5 choices

    { x = walker.x + choiceX
      y = walker.y + choiceY }

let toMouseStep p5 walker =
    let mouseX = mouseX p5 |> int
    let mouseY = mouseY p5 |> int

    let deltaX = if walker.x < mouseX then 1 else -1
    let deltaY = if walker.y < mouseY then 1 else -1

    { x = walker.x + deltaX
      y = walker.y + deltaY }

let update p5 walker =
    let action = if random.Next(2) = 0 then randomStep else toMouseStep
    action p5 walker

let draw p5 walker =
    // Adding .5 yields better results with regards to aliasing.
    point p5 (float walker.x + 0.5) (float walker.y + 0.5)

let run node = simulate node setup update draw
