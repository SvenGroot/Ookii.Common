# Workaround for: https://github.com/EWSoftware/SHFB/issues/1084 (ref struct documented as obsolete.)
# Fix the namespace topic page.
$helpPath = Join-Path $PSScriptRoot "Help" "html"
$file = Join-Path $helpPath "N_Ookii_Common.htm"
$lines = Get-Content $file -Raw
$lines -replace '<br /><span class="tag is-danger">Obsolete.</span>', '' | Set-Content $file

# Fix the type topic pages.
$names = "T_Ookii_Common_NullableReadOnlySpan_1",
    "T_Ookii_Common_NullableReadOnlySpanPair_2",
    "T_Ookii_Common_NullableSpan_1",
    "T_Ookii_Common_ReadOnlySpanPair_2"

foreach ($name in $names) {
    $file = Join-Path $helpPath "$name.htm"
    $lines = Get-Content $file -Raw
    $lines = $lines -replace '<div id="TopicNotices"><span class="tags"><span class="tag is-danger is-medium">Note: This API is now obsolete.</span></span></div>', ''
    $lines -replace '<span class="keyword">struct</span>', '<span class="keyword">ref</span> <span class="keyword">struct</span>' | Set-Content $file
}
