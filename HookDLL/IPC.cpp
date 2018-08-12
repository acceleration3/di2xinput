#include "stdafx.h"
#include "IPC.h"

#include "ControllerManager.h"

HANDLE IPC::pipeHandle = NULL;
std::thread IPC::pipeThread;

void IPC::PipeThread()
{
	bool readPipe = true;

	std::cout << "Started pipe thread." << std::endl;

	BOOL result = ConnectNamedPipe(pipeHandle, NULL);

	if (result || (!result && GetLastError() == ERROR_PIPE_CONNECTED))
	{
		std::cout << "Connected pipe." << std::endl;

		while (readPipe)
		{
			int messageLength = 0;
			DWORD bytesRead = 0;

			if (!ReadFile(pipeHandle, &messageLength, 4, &bytesRead, NULL))
			{
				std::cout << "Failed to read message length. Error: " << GetLastError() << std::endl;
				readPipe = false;
				continue;
			}

			std::vector<uint8_t> mappingBuffer(messageLength, '\x00');

			if (!ReadFile(pipeHandle, &mappingBuffer[0], messageLength, &bytesRead, NULL))
			{
				std::cout << "Failed to read message. Error: " << GetLastError() << std::endl;
				readPipe = false;
				continue;
			}

			ControllerManager::SetMappingsFromBuffer(mappingBuffer);
		}

		DisconnectNamedPipe(pipeHandle);
		std::cout << "Disconnected pipe." << std::endl;
	}
	else
	{
		std::cout << "Pipe failed to connect. Error: " << GetLastError() << std::endl;
	}
}

bool IPC::Init()
{
	std::wstring processName(MAX_PATH, '\0');
	GetModuleFileName(NULL, &processName[0], MAX_PATH);

	int lastSlash = processName.find_last_of('\\');
	int lastDot = processName.find_last_of('.');

	processName = processName.substr(lastSlash + 1,  (processName.length() - lastSlash) - (processName.length() - (lastDot - 1)));

	std::wstring pipeName = L"\\\\.\\pipe\\" + processName + L"_ipc";
	pipeHandle = CreateNamedPipeW(pipeName.c_str(), PIPE_ACCESS_DUPLEX, PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT, 1, 400, 400, NMPWAIT_USE_DEFAULT_WAIT, NULL);

	if (pipeHandle == INVALID_HANDLE_VALUE)
	{
		std::wcout << "Failed to create pipe. Error: " << GetLastError() << std::endl;
		return false;
	}

	std::wcout << "Pipe " << pipeName << " (" << std::hex << pipeHandle << ") created." << std::endl;

	pipeThread = std::thread(&IPC::PipeThread);

	return true;
}
