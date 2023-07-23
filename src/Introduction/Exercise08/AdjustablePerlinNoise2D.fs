//! Exercise I.8: Adjustable 2D Perlin noise
module NatureOfCode.Introduction.Exercise08.AdjustablePerlinNoise2D

open P5.Core
open P5.Rendering
open P5.Math
open P5.Color
open P5.Image
open P5.DOM
open P5.Structure

type Parameters = { detail: float; offsetRoc: float }

let mutable parameters = { detail = 4; offsetRoc = 0.01 }

let drawNoise p5 =
    let width = 720
    let height = 400
    resizeCanvas p5 width height

    loadPixels p5
    noiseDetail p5 parameters.detail 0.5

    for x in { 0 .. width - 1 } do
        for y in { 0 .. height - 1 } do
            let xOff = float x * parameters.offsetRoc
            let yOff = float y * parameters.offsetRoc

            let intensity = map p5 (noise2D p5 xOff yOff) 0 1 0 255
            setPixel p5 x y (Grayscale intensity)

    updatePixels p5

let setup p5 =
    let detailSlider = createSliderWithOptions p5 1 10 parameters.detail 1
    detailSlider.setParent (unbox <| createDiv p5 "Noise detail")

    detailSlider.changed (fun _ ->
        parameters <-
            { parameters with
                detail = detailSlider.getValue () }

        redraw p5)

    let offsetRocSlider = createSliderWithOptions p5 0.01 0.1 parameters.offsetRoc 0.01
    offsetRocSlider.setParent (unbox <| createDiv p5 "Offset rate of change")

    offsetRocSlider.changed (fun _ ->
        parameters <-
            { parameters with
                offsetRoc = offsetRocSlider.getValue () }

        redraw p5)

let draw p5 _ =
    noLoop p5
    drawNoise p5

let run node = animate node setup draw
