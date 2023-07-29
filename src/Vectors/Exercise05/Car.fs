//! Example 1.05: Accelerating car
module NatureOfCode.Vectors.Exercise05.Car

open P5.Core
open P5.Rendering
open P5.Color
open P5.Math
open P5.Environment
open P5.Shape
open P5.Image
open P5.Typography
open P5.Events

type State =
    | Accelerating
    | Braking
    | Idle

type Car =
    { location: P5Vector
      velocity: P5Vector
      acceleration: P5Vector
      topSpeed: float
      sprite: P5Image
      state: State }

let createCar p5 sprite =
    let width = width p5 |> float
    let height = height p5 |> float

    let location = P5Vector.create (width / 2.0, height - 100.0)
    let velocity = P5Vector.create (0, 0)
    let acceleration = P5Vector.create (0, 0)

    { location = location
      velocity = velocity
      acceleration = acceleration
      topSpeed = 20
      sprite = sprite
      state = Idle }

let updateCar p5 car =
    match car.state with
    | Accelerating -> car.acceleration.set (0.2)
    | Braking -> car.acceleration.set (-0.5)
    | Idle -> car.acceleration.set (-0.1)

    car.velocity.addVector car.acceleration
    car.velocity.limit car.topSpeed

    if car.velocity.x <= 0 then
        car.acceleration.set (0)
        car.velocity.set (0)

    car.location.addVector car.velocity

    let width = width p5 |> float

    car.location.x <-
        match car.location.x with
        | x when x > width -> 0
        | x when x < 0 -> width
        | x -> x

    car

let handleInput p5 (car: Car) =
    let state =
        if keyCodeIsDown p5 RightArrow then Accelerating
        else if keyCodeIsDown p5 LeftArrow then Braking
        else Idle

    { car with state = state }

let displayCar p5 car =
    strokeWeight p5 2
    stroke p5 (Grayscale 0)
    fill p5 (Grayscale 175)

    imageMode p5 ImageMode.Center
    image p5 car.sprite car.location.x car.location.y

let preload p5 = loadImage p5 "img/car.png"

let setup p5 carSprite =
    createCanvas p5 720 400
    createCar p5 carSprite

let update p5 car = handleInput p5 car |> updateCar p5

let draw p5 car =
    background p5 (Grayscale 255)

    text p5 "→ Accelerate" 10 20
    text p5 "← Brake" 10 40

    displayCar p5 car

let run node =
    simulateWithPreload node preload setup update draw
