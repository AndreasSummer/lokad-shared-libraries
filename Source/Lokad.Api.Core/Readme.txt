License
=======

Copyright (c) Lokad 2009 
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
    * Neither the name of Lokad nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

Lokad.API.Core
==============

This Open Source library contains .NET specific helper classes and routines implemented on top of the Lokad.Api.Interface.

This library features LokadService class that could be considered as an entry point to the entire Lokad API. This class is instantiated by the ServiceFactory.GetConnector and implement methods for working with the Lokad API. It also hides the complexity of:

    * Validating all the input values against the requirements of the Lokad API
    * Logging all operations to the provided log system
    * Handling all the exceptions with the centrally configured exception handling policy (this includes capturing specific exceptions into the log, exception counters and retrying on specific failures)
    * Configuring all the policies mentioned above (there are presets for production and development configs).
    * Performing the paging operations while retrieving large collections
    * Breaking up large data batches into the smaller ones, while saving them to the server.
    * Establishing proper communications channel (i.e. defining WebServices) with the Lokad API.



Life cycle
==========

This library could be updated frequently within the development life cycle to provide better functionality for .NET developers leveraging the Lokad API. Note, that generally these updates will not involve changes of the Lokad.Api.Interface, since the latter one is exposed to the wider audience, than Lokad.Api.Core. 

To enforce this behavior (while avoiding any costly mistakes), Core library does not reference Interface directly. Instead, it references precompiled Lokad.Api.Interface library that resides with the rest of the binaries in Resource\Library.

Lokad.Api.Core assembly has a strong name.
