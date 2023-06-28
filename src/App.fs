module App

open P5.Core
open P5.Environment
open P5.Color
open P5.Shape
open P5.Rendering
open P5.Math

type Walker = { x: int; y: int }

type Direction =
    | Up
    | Down
    | Left
    | Right

let random = new System.Random()

let setup p5 =
    createCanvas p5 720 200
    background p5 (Grayscale 255)
    stroke p5 (Grayscale 0)

    { x = width p5 / 2; y = height p5 / 2 }

let update p5 walker =
    let choices = [| Up; Down; Left; Right |]
    let choice = randomChoice p5 choices

    match choice with
    | Up -> { walker with y = walker.y - 1 }
    | Down -> { walker with y = walker.y + 1 }
    | Left -> { walker with x = walker.x - 1 }
    | Right -> { walker with x = walker.x + 1 }

let draw p5 walker =
    // Adding .5 yields better results with regards to aliasing.
    point p5 (float walker.x + 0.5) (float walker.y + 0.5)

simulate NoNode setup update draw
