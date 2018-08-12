#pragma once

class IPC
{
private:
	static std::thread pipeThread;
	static HANDLE pipeHandle;
	static void PipeThread();

public:
	static bool Init();
};

