#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
258,
270,
271,
272,
273,
274,
275,
276,
277,
278,
281,
282,
283,
457,
458,
459,
488,
489,
490,
510,
511,
512,
513,
630,
631,
632,
635,
681,
682,
683,
684,
685,
690,
692,
694,
696,
701,
709,
710,
711,
712,
713,
714,
715,
716,
717,
718,
719,
720,
721,
722,
723,
724,
725,
727,
728,
729,
730,
731,
732,
733,
829,
830,
831,
832,
833,
834,
835,
836,
837,
838,
839,
840,
841,
842,
843,
844,
845,
847,
848,
849,
850,
851,
852,
853,
920,
921,
989,
996,
999,
1001,
1006,
1007,
1009,
1010,
1014,
1015,
1017,
1019,
1020,
1023,
1024,
1025,
1028,
1030,
1033,
1035,
1037,
1046,
1113,
1115,
1117,
1127,
1128,
1129,
1130,
1132,
1139,
1140,
1141,
1142,
1143,
1151,
1152,
1153,
1157,
1158,
1160,
1164,
1165,
1166,
1450,
1642,
1643,
9779,
9780,
9782,
9783,
9784,
9785,
9786,
9788,
9790,
9792,
9793,
9804,
9806,
9813,
9815,
9817,
9819,
9870,
9871,
9873,
9874,
9875,
9876,
9877,
9879,
9881,
10962,
10966,
10968,
10969,
10970,
10971,
11234,
11235,
11236,
11237,
11255,
11256,
11257,
11259,
11305,
11370,
11372,
11374,
11383,
11384,
11385,
11386,
11387,
11825,
11826,
11831,
11832,
11866,
11886,
11893,
11900,
11911,
11915,
11940,
12019,
12021,
12032,
12034,
12035,
12036,
12043,
12057,
12077,
12078,
12086,
12088,
12095,
12096,
12099,
12101,
12106,
12112,
12113,
12120,
12122,
12134,
12137,
12138,
12139,
12150,
12159,
12165,
12166,
12167,
12169,
12170,
12187,
12189,
12203,
12224,
12225,
12250,
12255,
12285,
12286,
12828,
12842,
12935,
12936,
13151,
13152,
13159,
13160,
13161,
13167,
13264,
13963,
13964,
14612,
14613,
14614,
14619,
14629,
15582,
15603,
15605,
15607,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_InitializeInternal_raw (int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
void ves_icall_System_Enum_InternalBoxEnum_raw (int,int,int64_t,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
int ves_icall_System_GC_GetCollectionCount (int);
int ves_icall_System_GC_GetMaxGeneration ();
void ves_icall_System_GC_InternalCollect (int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Acos (double);
double ves_icall_System_Math_Acosh (double);
double ves_icall_System_Math_Asin (double);
double ves_icall_System_Math_Asinh (double);
double ves_icall_System_Math_Atan (double);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Atanh (double);
double ves_icall_System_Math_Cbrt (double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Cosh (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sinh (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_Tanh (double);
double ves_icall_System_Math_FusedMultiplyAdd (double,double,double);
double ves_icall_System_Math_Log2 (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Acos (float);
float ves_icall_System_MathF_Acosh (float);
float ves_icall_System_MathF_Asin (float);
float ves_icall_System_MathF_Asinh (float);
float ves_icall_System_MathF_Atan (float);
float ves_icall_System_MathF_Atan2 (float,float);
float ves_icall_System_MathF_Atanh (float);
float ves_icall_System_MathF_Cbrt (float);
float ves_icall_System_MathF_Ceiling (float);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Cosh (float);
float ves_icall_System_MathF_Exp (float);
float ves_icall_System_MathF_Floor (float);
float ves_icall_System_MathF_Log (float);
float ves_icall_System_MathF_Log10 (float);
float ves_icall_System_MathF_Pow (float,float);
float ves_icall_System_MathF_Sin (float);
float ves_icall_System_MathF_Sinh (float);
float ves_icall_System_MathF_Sqrt (float);
float ves_icall_System_MathF_Tan (float);
float ves_icall_System_MathF_Tanh (float);
float ves_icall_System_MathF_FusedMultiplyAdd (float,float,float);
float ves_icall_System_MathF_Log2 (float);
float ves_icall_System_MathF_ModF (float,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_System_RuntimeType_AllocateValueType_raw (int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsComObject_raw (int,int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_String_InternalIsInterned_raw (int,int);
int ves_icall_System_String_InternalIntern_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw (int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw (int,int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
int ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_CustomAttributeBuilder_GetBlob_raw (int,int,int,int,int,int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsAttached_internal ();
int ves_icall_System_Diagnostics_Debugger_IsLogging ();
void ves_icall_System_Diagnostics_Debugger_Log (int,int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 258,
ves_icall_System_Array_InternalCreate,
// token 270,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 271,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 272,
ves_icall_System_Array_CanChangePrimitive,
// token 273,
ves_icall_System_Array_FastCopy,
// token 274,
ves_icall_System_Array_GetLengthInternal_raw,
// token 275,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 276,
ves_icall_System_Array_GetGenericValue_icall,
// token 277,
ves_icall_System_Array_GetValueImpl_raw,
// token 278,
ves_icall_System_Array_SetGenericValue_icall,
// token 281,
ves_icall_System_Array_SetValueImpl_raw,
// token 282,
ves_icall_System_Array_InitializeInternal_raw,
// token 283,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 457,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 458,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 459,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 488,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 489,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 490,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 510,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 511,
ves_icall_System_Enum_InternalBoxEnum_raw,
// token 512,
ves_icall_System_Enum_InternalGetCorElementType,
// token 513,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 630,
ves_icall_System_Environment_get_ProcessorCount,
// token 631,
ves_icall_System_Environment_get_TickCount,
// token 632,
ves_icall_System_Environment_get_TickCount64,
// token 635,
ves_icall_System_Environment_FailFast_raw,
// token 681,
ves_icall_System_GC_GetCollectionCount,
// token 682,
ves_icall_System_GC_GetMaxGeneration,
// token 683,
ves_icall_System_GC_InternalCollect,
// token 684,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 685,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 690,
ves_icall_System_GC_SuppressFinalize_raw,
// token 692,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 694,
ves_icall_System_GC_GetGCMemoryInfo,
// token 696,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 701,
ves_icall_System_Object_MemberwiseClone_raw,
// token 709,
ves_icall_System_Math_Acos,
// token 710,
ves_icall_System_Math_Acosh,
// token 711,
ves_icall_System_Math_Asin,
// token 712,
ves_icall_System_Math_Asinh,
// token 713,
ves_icall_System_Math_Atan,
// token 714,
ves_icall_System_Math_Atan2,
// token 715,
ves_icall_System_Math_Atanh,
// token 716,
ves_icall_System_Math_Cbrt,
// token 717,
ves_icall_System_Math_Ceiling,
// token 718,
ves_icall_System_Math_Cos,
// token 719,
ves_icall_System_Math_Cosh,
// token 720,
ves_icall_System_Math_Exp,
// token 721,
ves_icall_System_Math_Floor,
// token 722,
ves_icall_System_Math_Log,
// token 723,
ves_icall_System_Math_Log10,
// token 724,
ves_icall_System_Math_Pow,
// token 725,
ves_icall_System_Math_Sin,
// token 727,
ves_icall_System_Math_Sinh,
// token 728,
ves_icall_System_Math_Sqrt,
// token 729,
ves_icall_System_Math_Tan,
// token 730,
ves_icall_System_Math_Tanh,
// token 731,
ves_icall_System_Math_FusedMultiplyAdd,
// token 732,
ves_icall_System_Math_Log2,
// token 733,
ves_icall_System_Math_ModF,
// token 829,
ves_icall_System_MathF_Acos,
// token 830,
ves_icall_System_MathF_Acosh,
// token 831,
ves_icall_System_MathF_Asin,
// token 832,
ves_icall_System_MathF_Asinh,
// token 833,
ves_icall_System_MathF_Atan,
// token 834,
ves_icall_System_MathF_Atan2,
// token 835,
ves_icall_System_MathF_Atanh,
// token 836,
ves_icall_System_MathF_Cbrt,
// token 837,
ves_icall_System_MathF_Ceiling,
// token 838,
ves_icall_System_MathF_Cos,
// token 839,
ves_icall_System_MathF_Cosh,
// token 840,
ves_icall_System_MathF_Exp,
// token 841,
ves_icall_System_MathF_Floor,
// token 842,
ves_icall_System_MathF_Log,
// token 843,
ves_icall_System_MathF_Log10,
// token 844,
ves_icall_System_MathF_Pow,
// token 845,
ves_icall_System_MathF_Sin,
// token 847,
ves_icall_System_MathF_Sinh,
// token 848,
ves_icall_System_MathF_Sqrt,
// token 849,
ves_icall_System_MathF_Tan,
// token 850,
ves_icall_System_MathF_Tanh,
// token 851,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 852,
ves_icall_System_MathF_Log2,
// token 853,
ves_icall_System_MathF_ModF,
// token 920,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 921,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 989,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 996,
ves_icall_RuntimeType_make_array_type_raw,
// token 999,
ves_icall_RuntimeType_make_byref_type_raw,
// token 1001,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 1006,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 1007,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 1009,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 1010,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 1014,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 1015,
ves_icall_System_RuntimeType_AllocateValueType_raw,
// token 1017,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 1019,
ves_icall_System_RuntimeType_getFullName_raw,
// token 1020,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 1023,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 1024,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 1025,
ves_icall_RuntimeType_GetFields_native_raw,
// token 1028,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 1030,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 1033,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 1035,
ves_icall_RuntimeType_GetName_raw,
// token 1037,
ves_icall_RuntimeType_GetNamespace_raw,
// token 1046,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1113,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1115,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1117,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1127,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1128,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1129,
ves_icall_RuntimeTypeHandle_IsComObject_raw,
// token 1130,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1132,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1139,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1140,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1141,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1142,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1143,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1151,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1152,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1153,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1157,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1158,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1160,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1164,
ves_icall_System_String_FastAllocateString_raw,
// token 1165,
ves_icall_System_String_InternalIsInterned_raw,
// token 1166,
ves_icall_System_String_InternalIntern_raw,
// token 1450,
ves_icall_System_Type_internal_from_handle_raw,
// token 1642,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1643,
ves_icall_System_ValueType_Equals_raw,
// token 9779,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 9780,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 9782,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 9783,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 9784,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 9785,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 9786,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 9788,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 9790,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 9792,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 9793,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 9804,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 9806,
mono_monitor_exit_icall_raw,
// token 9813,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 9815,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 9817,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 9819,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 9870,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 9871,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 9873,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 9874,
ves_icall_System_Threading_Thread_GetState_raw,
// token 9875,
ves_icall_System_Threading_Thread_SetState_raw,
// token 9876,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 9877,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 9879,
ves_icall_System_Threading_Thread_YieldInternal,
// token 9881,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 10962,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 10966,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 10968,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 10969,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 10970,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 10971,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 11234,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 11235,
ves_icall_System_GCHandle_InternalFree_raw,
// token 11236,
ves_icall_System_GCHandle_InternalGet_raw,
// token 11237,
ves_icall_System_GCHandle_InternalSet_raw,
// token 11255,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 11256,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 11257,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 11259,
ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw,
// token 11305,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 11370,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 11372,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw,
// token 11374,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 11383,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 11384,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 11385,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 11386,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw,
// token 11387,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 11825,
ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw,
// token 11826,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 11831,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 11832,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 11866,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 11886,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 11893,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 11900,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 11911,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 11915,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 11940,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 12019,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 12021,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 12032,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 12034,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 12035,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 12036,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 12043,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 12057,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 12077,
ves_icall_reflection_get_token_raw,
// token 12078,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 12086,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 12088,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 12095,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 12096,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 12099,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 12101,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 12106,
ves_icall_reflection_get_token_raw,
// token 12112,
ves_icall_get_method_info_raw,
// token 12113,
ves_icall_get_method_attributes,
// token 12120,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 12122,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 12134,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 12137,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 12138,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 12139,
ves_icall_reflection_get_token_raw,
// token 12150,
ves_icall_InternalInvoke_raw,
// token 12159,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 12165,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 12166,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 12167,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 12169,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 12170,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 12187,
ves_icall_InvokeClassConstructor_raw,
// token 12189,
ves_icall_InternalInvoke_raw,
// token 12203,
ves_icall_reflection_get_token_raw,
// token 12224,
ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw,
// token 12225,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 12250,
ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw,
// token 12255,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 12285,
ves_icall_reflection_get_token_raw,
// token 12286,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 12828,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 12842,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 12935,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 12936,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 13151,
ves_icall_ModuleBuilder_basic_init_raw,
// token 13152,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 13159,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 13160,
ves_icall_ModuleBuilder_getToken_raw,
// token 13161,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 13167,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 13264,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 13963,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 13964,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 14612,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 14613,
ves_icall_System_Diagnostics_Debugger_IsLogging,
// token 14614,
ves_icall_System_Diagnostics_Debugger_Log,
// token 14619,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 14629,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 15582,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 15603,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 15605,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 15607,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
0,
0,
0,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
};
