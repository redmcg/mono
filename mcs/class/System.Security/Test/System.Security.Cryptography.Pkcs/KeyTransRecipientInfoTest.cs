//
// KeyTransRecipientInfoTest.cs - NUnit tests for KeyTransRecipientInfo
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// (C) 2003 Motus Technologies Inc. (http://www.motus.com)
// Copyright (C) 2004-2005 Novell, Inc (http://www.novell.com)
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


using NUnit.Framework;

using System;
using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace MonoTests.System.Security.Cryptography.Pkcs {

	[TestFixture]
	public class KeyTransRecipientInfoTest {

		static private byte[] issuerAndSerialNumber = { 0x30, 0x82, 0x01, 0x1C, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x03, 0xA0, 0x82, 0x01, 0x0D, 0x30, 0x82, 0x01, 0x09, 0x02, 0x01, 0x00, 0x31, 0x81, 0xD6, 0x30, 0x81, 0xD3, 0x02, 0x01, 0x00, 0x30, 0x3C, 0x30, 0x28, 0x31, 0x26, 0x30, 0x24, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x1D, 0x4D, 0x6F, 0x74, 0x75, 0x73, 0x20, 0x54, 0x65, 0x63, 0x68, 0x6E, 0x6F, 0x6C, 0x6F, 0x67, 0x69, 0x65, 0x73, 0x20, 0x69, 0x6E, 0x63, 0x2E, 0x28, 0x74, 0x65, 0x73, 0x74, 0x29, 0x02, 0x10, 0x91, 0xC4, 0x4B, 0x0D, 0xB7, 0xD8, 0x10, 0x84, 0x42, 0x26, 0x71, 0xB3, 0x97, 0xB5, 0x00, 0x97, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00, 0x04, 0x81, 0x80, 0xCA, 0x4B, 0x97, 0x9C, 0xAB, 0x79, 0xC6, 0xDF, 0x6A, 0x27, 0xC7, 0x24, 0xC4, 0x5E, 0x3B, 0x31, 0xAD, 0xBC, 0x25, 0xE6, 0x38, 0x5E, 0x79, 0x26, 0x0E, 0x68, 0x46, 0x1D, 0x21, 0x81, 0x38, 0x92, 0xEC, 0xCB, 0x7C, 0x91, 0xD6, 0x09, 0x38, 0x91, 0xCE, 0x50, 0x5B, 0x70, 0x31, 0xB0, 0x9F, 0xFC, 0xE2, 0xEE, 0x45, 0xBC, 0x4B, 0xF8, 0x9A, 0xD9, 0xEE, 0xE7, 0x4A, 0x3D, 0xCD, 0x8D, 0xFF, 0x10, 0xAB, 0xC8, 0x19, 0x05, 0x54, 0x5E, 0x40, 0x7A, 0xBE, 0x2B, 0xD7, 0x22, 0x97, 0xF3, 0x23, 0xAF, 0x50, 0xF5, 0xEB, 0x43, 0x06, 0xC3, 0xFB, 0x17, 0xCA, 0xBD, 0xAD, 0x28, 0xD8, 0x10, 0x0F, 0x61, 0xCE, 0xF8, 0x25, 0x70, 0xF6, 0xC8, 0x1E, 0x7F, 0x82, 0xE5, 0x94, 0xEB, 0x11, 0xBF, 0xB8, 0x6F, 0xEE, 0x79, 0xCD, 0x63, 0xDD, 0x59, 0x8D, 0x25, 0x0E, 0x78, 0x55, 0xCE, 0x21, 0xBA, 0x13, 0x6B, 0x30, 0x2B, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x01, 0x30, 0x14, 0x06, 0x08, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x03, 0x07, 0x04, 0x08, 0x8C, 0x5D, 0xC9, 0x87, 0x88, 0x9C, 0x05, 0x72, 0x80, 0x08, 0x2C, 0xAF, 0x82, 0x91, 0xEC, 0xAD, 0xC5, 0xB5 };
		static private byte[] subjectKeyIdentifier = { 0x30, 0x81, 0xF2, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x03, 0xA0, 0x81, 0xE4, 0x30, 0x81, 0xE1, 0x02, 0x01, 0x02, 0x31, 0x81, 0xAE, 0x30, 0x81, 0xAB, 0x02, 0x01, 0x02, 0x80, 0x14, 0x02, 0xE1, 0xA7, 0x32, 0x54, 0xAE, 0xFD, 0xC0, 0xA4, 0x32, 0x36, 0xF6, 0xFE, 0x23, 0x6A, 0x03, 0x72, 0x28, 0xB1, 0xF7, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00, 0x04, 0x81, 0x80, 0x4E, 0x0C, 0xA0, 0x9D, 0x79, 0xD0, 0x31, 0x12, 0x96, 0x32, 0xD5, 0x9B, 0x51, 0x49, 0xDC, 0xC5, 0xC4, 0xFB, 0xFE, 0xE8, 0x33, 0x11, 0x13, 0xBE, 0x48, 0x02, 0x5D, 0x99, 0x9D, 0xB5, 0xAC, 0x52, 0xA3, 0xE3, 0xDE, 0x1B, 0x88, 0x00, 0x7C, 0x3E, 0xD4, 0xFE, 0x93, 0x6A, 0x93, 0x03, 0x04, 0x73, 0xA9, 0x22, 0x3E, 0xD5, 0x2A, 0xEE, 0xD7, 0xFC, 0xFB, 0xB4, 0xFF, 0xD4, 0x9B, 0x32, 0x4F, 0xB3, 0x1E, 0x8E, 0xBA, 0xF7, 0xD3, 0x12, 0x07, 0x19, 0xB8, 0x28, 0x57, 0xC4, 0x54, 0x33, 0x14, 0x83, 0x77, 0xA6, 0x14, 0x00, 0xF2, 0x02, 0xA9, 0x9B, 0x45, 0xF3, 0xAB, 0x41, 0x00, 0x69, 0xE2, 0xB3, 0xD0, 0xB9, 0xA3, 0x2D, 0x9E, 0x29, 0x7F, 0xBC, 0xAE, 0x92, 0x05, 0x11, 0x5A, 0x06, 0xB7, 0x26, 0x83, 0x0A, 0x33, 0x32, 0x6E, 0x5F, 0x4A, 0x5D, 0x32, 0x2E, 0x51, 0xD4, 0xE9, 0xD5, 0x17, 0xC9, 0x30, 0x2B, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x01, 0x30, 0x14, 0x06, 0x08, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x03, 0x07, 0x04, 0x08, 0x55, 0x62, 0xB4, 0x77, 0x9A, 0x99, 0x39, 0xEF, 0x80, 0x08, 0x76, 0x3A, 0x9B, 0x63, 0x46, 0x95, 0xDB, 0xFA };

		private KeyTransRecipientInfo GetKeyTransRecipientInfo (byte[] encoded) 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			ep.Decode (encoded);
			return (KeyTransRecipientInfo) ep.RecipientInfos [0];
		}

		[Test]
		public void IssuerAndSerialNumber () 
		{
			KeyTransRecipientInfo ktri = GetKeyTransRecipientInfo (issuerAndSerialNumber);
			Assert.AreEqual ("CA-4B-97-9C-AB-79-C6-DF-6A-27-C7-24-C4-5E-3B-31-AD-BC-25-E6-38-5E-79-26-0E-68-46-1D-21-81-38-92-EC-CB-7C-91-D6-09-38-91-CE-50-5B-70-31-B0-9F-FC-E2-EE-45-BC-4B-F8-9A-D9-EE-E7-4A-3D-CD-8D-FF-10-AB-C8-19-05-54-5E-40-7A-BE-2B-D7-22-97-F3-23-AF-50-F5-EB-43-06-C3-FB-17-CA-BD-AD-28-D8-10-0F-61-CE-F8-25-70-F6-C8-1E-7F-82-E5-94-EB-11-BF-B8-6F-EE-79-CD-63-DD-59-8D-25-0E-78-55-CE-21-BA-13-6B", BitConverter.ToString (ktri.EncryptedKey), "EncryptedKey");
			Assert.AreEqual (0, ktri.KeyEncryptionAlgorithm.KeyLength, "KeyEncryptionAlgorithm.KeyLength");
			Assert.AreEqual ("RSA", ktri.KeyEncryptionAlgorithm.Oid.FriendlyName, "KeyEncryptionAlgorithm.Oid.FriendlyName");
			Assert.AreEqual ("1.2.840.113549.1.1.1", ktri.KeyEncryptionAlgorithm.Oid.Value, "KeyEncryptionAlgorithm.Oid.Value");
			Assert.AreEqual (0, ktri.KeyEncryptionAlgorithm.Parameters.Length, "KeyEncryptionAlgorithm.Parameters");
			Assert.AreEqual (SubjectIdentifierType.IssuerAndSerialNumber, ktri.RecipientIdentifier.Type, "RecipientIdentifier.Type");
			X509IssuerSerial xis = (X509IssuerSerial) ktri.RecipientIdentifier.Value;
			Assert.AreEqual ("CN=Motus Technologies inc.(test)", xis.IssuerName, "RecipientIdentifier.Value.IssuerName");
			Assert.AreEqual ("91C44B0DB7D81084422671B397B50097", xis.SerialNumber, "RecipientIdentifier.Value.SerialNumber");
			Assert.AreEqual (RecipientInfoType.KeyTransport, ktri.Type, "Type");
			Assert.AreEqual (0, ktri.Version, "Version");
		}

		[Test]
		[Category ("NotWorking")]
		public void SubjectKeyIdentifier () 
		{
			KeyTransRecipientInfo ktri = GetKeyTransRecipientInfo (subjectKeyIdentifier);
			Assert.AreEqual ("4E-0C-A0-9D-79-D0-31-12-96-32-D5-9B-51-49-DC-C5-C4-FB-FE-E8-33-11-13-BE-48-02-5D-99-9D-B5-AC-52-A3-E3-DE-1B-88-00-7C-3E-D4-FE-93-6A-93-03-04-73-A9-22-3E-D5-2A-EE-D7-FC-FB-B4-FF-D4-9B-32-4F-B3-1E-8E-BA-F7-D3-12-07-19-B8-28-57-C4-54-33-14-83-77-A6-14-00-F2-02-A9-9B-45-F3-AB-41-00-69-E2-B3-D0-B9-A3-2D-9E-29-7F-BC-AE-92-05-11-5A-06-B7-26-83-0A-33-32-6E-5F-4A-5D-32-2E-51-D4-E9-D5-17-C9", BitConverter.ToString (ktri.EncryptedKey), "EncryptedKey");
			Assert.AreEqual (0, ktri.KeyEncryptionAlgorithm.KeyLength, "KeyEncryptionAlgorithm.KeyLength");
			Assert.AreEqual ("RSA", ktri.KeyEncryptionAlgorithm.Oid.FriendlyName, "KeyEncryptionAlgorithm.Oid.FriendlyName");
			Assert.AreEqual ("1.2.840.113549.1.1.1", ktri.KeyEncryptionAlgorithm.Oid.Value, "KeyEncryptionAlgorithm.Oid.Value");
			Assert.AreEqual (0, ktri.KeyEncryptionAlgorithm.Parameters.Length, "KeyEncryptionAlgorithm.Parameters");
			Assert.AreEqual (SubjectIdentifierType.SubjectKeyIdentifier, ktri.RecipientIdentifier.Type, "RecipientIdentifier.Type");
			Assert.AreEqual ("02E1A73254AEFDC0A43236F6FE236A037228B1F7", (string)ktri.RecipientIdentifier.Value, "RecipientIdentifier.Value");
			Assert.AreEqual (RecipientInfoType.KeyTransport, ktri.Type, "Type");
			Assert.AreEqual (2, ktri.Version, "Version");
		}

		[Test]
		public void EncryptedKey_ModifyContent ()
		{
			KeyTransRecipientInfo ktri = GetKeyTransRecipientInfo (issuerAndSerialNumber);
			Assert.AreEqual ("CA-4B-97-9C-AB-79-C6-DF-6A-27-C7-24-C4-5E-3B-31-AD-BC-25-E6-38-5E-79-26-0E-68-46-1D-21-81-38-92-EC-CB-7C-91-D6-09-38-91-CE-50-5B-70-31-B0-9F-FC-E2-EE-45-BC-4B-F8-9A-D9-EE-E7-4A-3D-CD-8D-FF-10-AB-C8-19-05-54-5E-40-7A-BE-2B-D7-22-97-F3-23-AF-50-F5-EB-43-06-C3-FB-17-CA-BD-AD-28-D8-10-0F-61-CE-F8-25-70-F6-C8-1E-7F-82-E5-94-EB-11-BF-B8-6F-EE-79-CD-63-DD-59-8D-25-0E-78-55-CE-21-BA-13-6B", BitConverter.ToString (ktri.EncryptedKey), "EncryptedKey");
			ktri.EncryptedKey[0] = 0x00;
			Assert.AreEqual ("00-4B-97-9C-AB-79-C6-DF-6A-27-C7-24-C4-5E-3B-31-AD-BC-25-E6-38-5E-79-26-0E-68-46-1D-21-81-38-92-EC-CB-7C-91-D6-09-38-91-CE-50-5B-70-31-B0-9F-FC-E2-EE-45-BC-4B-F8-9A-D9-EE-E7-4A-3D-CD-8D-FF-10-AB-C8-19-05-54-5E-40-7A-BE-2B-D7-22-97-F3-23-AF-50-F5-EB-43-06-C3-FB-17-CA-BD-AD-28-D8-10-0F-61-CE-F8-25-70-F6-C8-1E-7F-82-E5-94-EB-11-BF-B8-6F-EE-79-CD-63-DD-59-8D-25-0E-78-55-CE-21-BA-13-6B", BitConverter.ToString (ktri.EncryptedKey), "EncryptedKey");
			// this is a reference (not a copy) of the key
		}
	}
}

