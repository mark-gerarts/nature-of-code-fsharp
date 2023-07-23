//! Exercise I.10: Perlin landscape
module NatureOfCode.Introduction.Exercise10.PerlinLandscape

open P5.Core
open P5.Rendering
open P5.Math
open P5.Color
open P5.ThreeD
open P5.Shape
open P5.Transform

let width = 720
let height = 400

let planeWidth = 20
let planeHeight = 20

type Point = { x: float; y: float; z: float }

type Plane = Map<(int * int), float>

let setup p5 =
    createWebGLCanvas p5 width height

    let initPointFromIndex i =
        let x = i / planeHeight
        let y = i % planeHeight

        let xOff = float x * 0.1
        let yOff = float y * 0.1

        let coord = (x, y)
        let z = map p5 (noise2D p5 xOff yOff) 0 1 0 10

        (coord, z)

    let plane = Array.init (planeWidth * planeHeight) initPointFromIndex |> Map.ofArray

    let camera = createCamera p5
    camera.setPosition 0 200 120
    camera.lookAt 0 0 0

    plane

let draw p5 (plane: Plane) =
    background p5 (Grayscale 255)
    orbitControl p5
    let d = 10.0

    fill p5 (Grayscale 180)
    translate p5 (-(float planeWidth) * d / 2.0) (-(float planeHeight) * d / 2.0)

    for x in { 0 .. planeWidth - 2 } do
        for y in { 0 .. planeHeight - 2 } do
            let xf = float x
            let yf = float y

            let p1 =
                {| x = xf
                   y = yf
                   z = Map.find (x, y) plane |}

            let p2 =
                {| x = xf
                   y = yf + 1.0
                   z = Map.find (x, y + 1) plane |}

            let p3 =
                {| x = xf + 1.0
                   y = yf + 1.0
                   z = Map.find (x + 1, y + 1) plane |}

            let p4 =
                {| x = xf + 1.0
                   y = yf
                   z = Map.find (x + 1, y) plane |}

            beginShape p5
            vertex3D p5 (p1.x * d) (p1.y * d) (p1.z * d)
            vertex3D p5 (p2.x * d) (p2.y * d) (p2.z * d)
            vertex3D p5 (p3.x * d) (p3.y * d) (p3.z * d)
            vertex3D p5 (p4.x * d) (p4.y * d) (p4.z * d)
            endShapeAndClose p5

let run node = simulate node setup noUpdate draw
