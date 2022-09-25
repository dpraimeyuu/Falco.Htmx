namespace Falco.Htmx

open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Primitives

[<Extension>]
type HttpContextExtensions =
    [<Extension>]
    static member inline HXTrigger(ctx: HttpContext) =
        match ctx.Request.Headers.TryGetValue "HX-Trigger" with
        | (true, value) ->
            match value |> Seq.tryHead with
            | Some value -> value |> ValueOption.ofObj
            | None -> ValueNone
        | (false, _) -> ValueNone

    [<Extension>]
    static member inline HXTriggerName(ctx: HttpContext) =
        match ctx.Request.Headers.TryGetValue "HX-Trigger-Name" with
        | (true, value) ->
            match value |> Seq.tryHead with
            | Some value -> value |> ValueOption.ofObj
            | None -> ValueNone
        | (false, _) -> ValueNone

    [<Extension>]
    static member inline HXTarget(ctx: HttpContext) =
        match ctx.Request.Headers.TryGetValue "HX-Target" with
        | (true, value) ->
            match value |> Seq.tryHead with
            | Some value -> value |> ValueOption.ofObj
            | None -> ValueNone
        | (false, _) -> ValueNone

    [<Extension>]
    static member inline HXPrompt(ctx: HttpContext) =
        match ctx.Request.Headers.TryGetValue "HX-Prompt" with
        | (true, value) ->
            match value |> Seq.tryHead with
            | Some value -> value |> ValueOption.ofObj
            | None -> ValueNone
        | (false, _) -> ValueNone

    [<Extension>]
    static member inline IsHtmx(ctx: HttpContext) =
        match ctx.Request.Headers.TryGetValue "HX-Request" with
        | (true, value) ->
            match value |> Seq.tryHead |> Option.map bool.TryParse with
            | Some (true, value) -> value
            | Some (false, _) -> false
            | None -> false
        | (false, _) -> false

    [<Extension>]
    static member inline SetHXPush(ctx: HttpContext, url: string) =
        ctx.Response.Headers.Add("HX-Push", StringValues(url))

    [<Extension>]
    static member inline SetHXRedirect(ctx: HttpContext, url: string) =
        ctx.Response.Headers.Add("HX-Redirect", StringValues(url))

    [<Extension>]
    static member inline SetHXTrigger(ctx: HttpContext, trigger: string) =
        ctx.Response.Headers.Add("HX-Trigger", StringValues(trigger))

    [<Extension>]
    static member inline SetHXTrigger<'T>(ctx: HttpContext, trigger: string, detail: 'T) =
        let opts = Json.JsonOptions()
        opts.SerializerOptions.WriteIndented <- false
        let event = [ trigger, detail ] |> Map.ofList

        let payload =
            System.Text.Json.JsonSerializer.Serialize(event, opts.SerializerOptions)

        ctx.Response.Headers.Add("HX-Trigger", StringValues(payload))

    [<Extension>]
    static member inline SetHXAfterSwap(ctx: HttpContext) =
        ctx.Response.Headers.Add("HX-Trigger-After-Swap", StringValues("true"))

    [<Extension>]
    static member inline SetHXAfterSettle(ctx: HttpContext) =
        ctx.Response.Headers.Add("HX-Trigger-After-Settle", StringValues("true"))

    [<Extension>]
    static member inline SetHXRefresh(ctx: HttpContext) =
        ctx.Response.Headers.Add("HX-Refresh", StringValues("true"))