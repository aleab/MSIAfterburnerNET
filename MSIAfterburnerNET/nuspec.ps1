param([String] $nuspec)

$content = [System.IO.File]::ReadAllText($nuspec) -Replace '(?ms)\s*<files>.*?</files>', ''
[System.IO.File]::WriteAllText($nuspec, $content)
