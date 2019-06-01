//
// X509Store.cs: Handles a X.509 certificates/CRLs store
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//	Pablo Ruiz <pruiz@netway.org>
//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
// (C) 2010 Pablo Ruiz.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Security.Cryptography;

using Mono.Security.Cryptography;
using Mono.Security.X509.Extensions;

using SSCX = System.Security.Cryptography.X509Certificates;

using StoreLocation = System.Security.Cryptography.X509Certificates.StoreLocation;

namespace Mono.Security.X509 {

#if INSIDE_CORLIB || INSIDE_SYSTEM
	internal
#else
	public 
#endif
	class X509Store {

		private string _name;
		private StoreLocation _location;
		//private SafeCertSoreHandle _handle; 

		internal X509Store (string name, StoreLocation location)
		{
			_name = name;
			_location = location;
			// TODO: Open handle here using CertOpenSystemStore
		}

		// properties

		public X509CertificateCollection Certificates {
			get { 
				throw new NotImplementedException ("Mono.Security.X509.X509Store.get_Certificates");
			}
		}

		public ArrayList Crls {
			get {
				throw new NotImplementedException ("Mono.Security.X509.X509Store.get_Crls");
			}
		}

		public string Name {
			get {
				return _name;
			}
		}

		// methods

		public void Clear () 
		{
			throw new NotImplementedException ("Mono.Security.X509.X509Store.Clear()");
		}

		public void Close ()
		{
			throw new NotImplementedException ("Mono.Security.X509.X509Store.Close()");
		}

		public void Import (X509Certificate certificate) 
		{
			throw new NotImplementedException ("Mono.Security.X509.X509Store.Import(X509Certificate)");
		}

		public void Import (X509Crl crl) 
		{
			throw new NotImplementedException ("Mono.Security.X509.X509Store.Import(X509Crl)");
		}

		public void Remove (X509Certificate certificate) 
		{
			throw new NotImplementedException ("Mono.Security.X509.X509Store.Remove(X509Certificate)");
		}

		public void Remove (X509Crl crl) 
		{
			throw new NotImplementedException ("Mono.Security.X509.X509Store.Remove(X509Crl)");
		}
	}
}
