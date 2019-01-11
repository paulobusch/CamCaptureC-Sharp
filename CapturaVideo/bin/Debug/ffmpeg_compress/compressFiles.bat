set path_media=%1
set path_sourc=%2
cd %path_sourc%
shift 
for %%a in (%path_media%) do ffmpeg_compress\ffmpeg -y -i "%%a" "%%~a.avi" && del/q "%%a"