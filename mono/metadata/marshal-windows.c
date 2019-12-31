/**
 * \file
 * Windows marshal support.
 *
 * Copyright 2016 Microsoft
 * Licensed under the MIT license. See LICENSE file in the project root for full license information.
 */
#include <config.h>
#include <glib.h>

#if defined(HOST_WIN32)

#include <winsock2.h>
#include <windows.h>
#include <objbase.h>
#include "mono/metadata/marshal-windows-internals.h"
#include "icall-decl.h"

#if G_HAVE_API_SUPPORT(HAVE_CLASSIC_WINAPI_SUPPORT)

void*
mono_marshal_alloc_hglobal (size_t size)
{
	return GlobalAlloc (GMEM_FIXED, size);
}

gpointer
mono_marshal_realloc_hglobal (gpointer ptr, size_t size)
{
	return GlobalReAlloc (ptr, size, GMEM_MOVEABLE);
}

void
mono_marshal_free_hglobal (gpointer ptr)
{
	GlobalFree (ptr);
}

#endif /* G_HAVE_API_SUPPORT(HAVE_CLASSIC_WINAPI_SUPPORT) */

void*
mono_marshal_alloc_co_task_mem (size_t size)
{
	return CoTaskMemAlloc (size);
}

void
mono_marshal_free_co_task_mem (void *ptr)
{
	CoTaskMemFree (ptr);
}

gpointer
mono_marshal_realloc_co_task_mem (gpointer ptr, size_t size)
{
	return CoTaskMemRealloc (ptr, size);
}

char*
ves_icall_System_Runtime_InteropServices_Marshal_StringToHGlobalAnsi (const gunichar2 *s, int length);

char*
ves_icall_System_Runtime_InteropServices_Marshal_StringToHGlobalAnsi (const gunichar2 *s, int length)
{
	g_assert_not_netcore ();

	ERROR_DECL (error);
	size_t len = WideCharToMultiByte (CP_ACP, 0, s, length, NULL, 0, NULL, NULL);
	char* res = mono_marshal_alloc_hglobal_error (len+1, error);
	if (!res)
	{
		mono_error_set_pending_exception (error);
		return NULL;
	}

	WideCharToMultiByte (CP_ACP, 0, s, length, res, len, NULL, NULL);
	res[len] = 0;
	return res;
}

gpointer
mono_string_to_utf8str_impl (MonoStringHandle s, MonoError *error)
{
	char *as, *tmp;
	glong len;
	GError *gerror = NULL;

	if (MONO_HANDLE_IS_NULL (s))
		return NULL;

	if (!mono_string_handle_length (s)) {
		as = (char*)CoTaskMemAlloc (1);
		g_assert (as);
		as [0] = '\0';
		return as;
	}

	// FIXME pass g_utf16_to_utf8 an allocator to avoid double alloc/copy.

	uint32_t gchandle = 0;
	tmp = g_utf16_to_utf8 (mono_string_handle_pin_chars (s, &gchandle), mono_string_handle_length (s), NULL, &len, &gerror);
	mono_gchandle_free_internal (gchandle);
	if (gerror) {
		mono_error_set_argument (error, "string", gerror->message);
		g_error_free (gerror);
		return NULL;
	} else {
		as = (char*)CoTaskMemAlloc (len + 1);
		g_assert (as);
		memcpy (as, tmp, len + 1);
		g_free (tmp);
		return as;
	}
}

#endif /* HOST_WIN32 */
