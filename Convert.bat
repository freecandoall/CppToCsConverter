set SRC_ROOT=TestConvert\InputCpp\
set DST_ROOT=TestConvert\OutputCs\

set RUN=PacketGenerator\bin\Debug\PacketGenerator.exe

set arg=%SRC_DEF_ROOT%#%DST_ROOT%#Type.h
set arg=%arg% %SRC_PKT_ROOT%#%DST_ROOT%#RootCode.h
set arg=%arg% %SRC_PKT_ROOT%#%DST_ROOT%#PacketCode.h
set arg=%arg% %SRC_PKT_ROOT%#%DST_ROOT%#Connect.h
set arg=%arg% %SRC_PKT_ROOT%#%DST_ROOT%#Account.h

%RUN% %arg%

pause