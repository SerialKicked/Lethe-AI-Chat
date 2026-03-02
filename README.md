# Lethe AI Chat

Windows-based high-performance front-end for [KoboldCPP](https://github.com/LostRuins/koboldcpp) and OpenAI API compatible backends like [LM Studio](https://lmstudio.ai/) written in C#.NET. Using [Lethe AI](https://github.com/SerialKicked/Lethe-AI-Sharp) to do the heavy lifting.

<img width="1920" height="1032" alt="LetheChat_QrSGcp5ZBb" src="https://github.com/user-attachments/assets/7a81b84c-4c64-4249-9dfc-e5e210213787" />

## Main Features:
- All-in-one program to edit characters, system prompts, instruction formats, and inference settings
- Chatlogs are divided into sessions, ability to switch back to previous sessions, insert or delete individual sessions
- Chat sessions are automatically summarised and formatted for easy access
- Optional strong encryption of chatlogs and characters' memory systems
- Compatible with CoT / Thinking models, implemented in a non-obstructive fashion
- Extensive **long-term memory (LTM) system**
  - Vector Search done on previous sessions to retrieve contextual information based on user prompt
  - Space can be reserved to insert the summaries of the last X sessions in chronological order, improving the model's contextual awareness notably
  - Keyword-activated text insertion (or **World Info**) which can also be triggered using a keyword-less vector search
  - Learns about the user over time
  - Can run background tasks when the user is AFK to autonomously search the web and set goals based on past discussions
- Characters can use a configurable part of the system prompt to write a small journal entry that changes over time based on their interactions with the user
- TTS (text-to-speech) support through KoboldCPP API
- Image recognition support (assuming your backend and your model support it)
- Ability for the bot to augment its responses by searching on DuckDuckGo or Brave Search
- Automatic insertion of dates into the prompt to give the model a better sense of time (toggle)
- Import chatlogs and world info files from Silly Tavern
- Powerful **Group Chat** support (add/remove personas to a chat at will)
- Simple and intuitive UI

## Extremely Configurable

<img width="1333" height="705" alt="LetheChat_lQV04N078s" src="https://github.com/user-attachments/assets/9f325bf8-8cea-4cf5-b12b-0bbb1ddf0383" />

Lethe AI Chat offers a ton of advanced settings allowing you to configure the app for pretty much any use case.

## Supported Backend API
- **KoboldCPP API**: Recommended, with the most functionalities
- **OpenAI API**: Compatible with most backends, much less functionalities
- **Internal LetheAI Backend**: Run the program in a self-contained mode, allowing you to load GGUF files directly from the app

This application is designed for local deployment. The backend must be running on your computer or on a local network. The following backends have been tested: KoboldCpp, LM Studio, and Text Generation Web UI.

## Current Limitations
- No support for those drag & drop character cards/images (not planned, I use a very different system)

## Notes and Requirements
- Runs on Windows 10+
- To take full advantage of the LTM and dynamic character systems, a context window of at least 10K is required, 16K+ is *heavily* recommended
- Small models (<=8B params), and models with poor instruction following, may struggle with some of the features
- You can find a list of recommended models in my [Hugging Face Collections](https://huggingface.co/SerialKicked/collections)
- For TTS, KoboldCPP API is required

## License

Lethe AI Chat is under [CC BY-NC-SA 4.0](https://github.com/SerialKicked/ChatAI/tree/master?tab=License-1-ov-file#readme). You may not use this code for commercial purposes.
