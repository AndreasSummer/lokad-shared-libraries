Lokad.Client
============

At the present this project hosts following
logically separate sub-projects:

Lokad.Client
------------
Models, interfaces and controllers that do not belong to any
specific presentation framework (WPF, Silverlight, or Windows.Forms)
and implement core business functionality of client applications
leveraging Lokad forecasting technologies


Lokad.Client.Forms
------------------

Windows.Forms controls and forms that implement interfaces defined in
the Lokad.Client


Later on, platform-independent functionality (if found to be reusable)
might go into the Lokad.Shared.dll (generic helpers, delegates, 
interfaces etc), while adding additional specific project:

Lokad.Shared.Forms
------------------
Shared project that implements UI compositional functionality specific 
to Windows.Forms. It could hold classes such as:

* Validator - component for binding RuleMessages to Windows.Forms UI in 
a strongly-typed manner.
* Workspace - simple workspace for showing up dialogs and child windows
in Lokad UI Composition
* Control extensions