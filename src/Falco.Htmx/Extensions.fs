namespace Falco.Htmx

open Falco

/// htmx Request Headers
type HtmxRequestHeaders =
    { /// Indicates that the request is via an element using hx-boost
      HxBoosted : string option
      /// The current URL of the browser
      HxCurrentUrl : string option
      /// True if the request is for history restoration after a miss in the local history cache
      HxHistoryRestoreRequest : string option
      /// The user response to an hx-prompt
      HxPrompt : string option
      /// Always true
      HxRequest : string option
      /// The id of the target element if it exists
      HxTarget : string option
      /// The name of the triggered element if it exists
      HxTriggerName : string option
      /// The id of the triggered element if it exists
      HxTrigger : string option }

module Request =
    let getHtmxHeaders (next : HtmxRequestHeaders -> HttpHandler) : HttpHandler = fun ctx ->
        let headers = Request.getHeaders ctx
        let htmxHeaders =
            { HxBoosted = headers.TryGet "HX-Boosted"
              HxCurrentUrl = headers.TryGet "HX-Current-URL"
              HxHistoryRestoreRequest = headers.TryGet "HX-History-Restore-Request"
              HxPrompt = headers.TryGet "HX-Prompt"
              HxRequest = headers.TryGet "HX-Request"
              HxTarget = headers.TryGet "HX-Target"
              HxTriggerName = headers.TryGet "HX-Trigger-Name"
              HxTrigger = headers.TryGet "HX-Trigger" }
        next htmxHeaders ctx

type AjaxContext(event, ?source, ?handler, ?target, ?swap, ?values, ?headers) =
    /// An event that "triggered" the request
    member _.Event : string = event
    /// The source element of the request
    member _.Source : string option = source
    /// A callback that will handle the response HTML
    member _.Handler : string option = handler
    /// The target to swap the response into
    member _.Target : string option = target
    /// How the response will be swapped in relative to the target
    member _.Swap : string option = swap
    /// Values to submit with the request
    member _.Values : string option = values
    /// Headers to submit with the request
    member _.Headers : string option = headers

module Response =
    open System.Text.Json

    let [<Literal>] private _trueValue = "true"

    let private defaultJsonOptions =
        let options = JsonSerializerOptions()
        options.AllowTrailingCommas <- true
        options.PropertyNameCaseInsensitive <- true
        options

    // Allows you to do a client-side redirect that does not do a full page reload
    let withHxLocation (ctx : AjaxContext) : HttpResponseModifier =
        Response.withHeaders [
            "HX-Location", JsonSerializer.Serialize(ctx, defaultJsonOptions) ]

    // Pushes a new url into the history stack
    let withHxPushUrl (url: string) : HttpResponseModifier =
        Response.withHeaders [ "HX-Push-Url", url ]

    // Can be used to do a client-side redirect to a new location
    let withHxRedirect (url: string) : HttpResponseModifier =
        Response.withHeaders [ "HX-Redirect", url ]

    // If set to "true" the client side will do a a full refresh of the page
    let withHxRefresh : HttpResponseModifier =
        Response.withHeaders [ "HX-Refresh", _trueValue ]

    // Replaces the current URL in the location bar
    let withHxReplaceUrl (url: string) : HttpResponseModifier =
        Response.withHeaders [ "HX-Replace-Url", url ]

    // Allows you to specify how the response will be swapped. See hx-swap for possible values
    let withHxReswap (option : SwapOption) =
        Response.withHeaders [ "HX-Reswap", SwapOption.AsString option ]

    // A CSS selector that updates the target of the content update to a different element on the page
    let withHxRetarget (option : TargetOption) =
        Response.withHeaders [ "HX-Retarget", TargetOption.AsString option ]

    // // Allows you to trigger client side events, see the documentation for more info
    // let withHxTrigger<'T>(event: string, detail : 'T option) : HttpResponseModifier =
    //     let headerValue =
    //         match detail with
    //         | Some detail' ->
    //             [ event, detail ]
    //             |> Map.ofList
    //             |> fun x -> JsonSerializer.Serialize(x, defaultJsonOptions)

    //         | None ->
    //             event

    //     Response.withHeaders [ "HX-Trigger", headerValue ]

    // Allows you to trigger client side events, see the documentation for more info
    let withHxTriggerAfterSettle : HttpResponseModifier =
        Response.withHeaders [ "HX-Trigger-After-Settle", _trueValue ]

    // Allows you to trigger client side events, see the documentation for more info
    let withHxTriggerAfterSwap : HttpResponseModifier =
        Response.withHeaders [ "HX-Trigger-After-Swap", _trueValue ]