# Lethe AI Chat

Windows-based high-performance front-end for [KoboldCPP](https://github.com/LostRuins/koboldcpp) and OpenAI API written in C#.NET. Using [Lethe AI](https://github.com/SerialKicked/Lethe-AI-Sharp) to do the heavy lifting.

![WaifuAI_RjiqpbMiTq](https://github.com/user-attachments/assets/a4d1595d-a5f4-41f6-885e-5c50cd8fa619)

## Main Features:
- All-in-one program to edit characters, system prompts, instruction formats, and inference settings
- Chatlogs are divided into sessions, ability to switch back to previous sessions, insert or delete individual sessions
- Chat sessions are automatically summarised and formatted for easy access
- Compatible with CoT / Thinking models, implemented in a non-obstructive fashion
- Extensive **long-term memory (LTM) system**
  - Vector Search done on previous sessions to retrieve contextual information based on user prompt
  - Space can be reserved to insert the summaries of the last X sessions in chronological order, improving the model's contextual awareness notably
  - Keyword-activated text insertion (or **World Info**) which can also be triggered using a keyword-less vector search
- Characters can use a configurable part of the system prompt to write a small journal entry that changes over time based on their interactions with the user
- TTS (text-to-speech) support through KoboldCPP API
- Ability for the bot to augment its responses by searching on DuckDuckGo through KoboldCPP API (toggle)
- Customizable system allowing the bot to browse user-defined websites in search of requested information
- Automatic insertion of dates into the prompt to give the model a better sense of time (toggle)
- Ability for the bot to initiate chat (toggle)
- Import chatlogs and world info files from Silly Tavern
- Simple and intuitive UI

## Supported Backend API
- **KoboldCPP API**: Recommended, with the most functionalities
- **OpenAI API**: Compatible with most backends, much less functionalities

This application is designed for local deployment. The backend must be running on your computer or on a local network. The following backends have been tested: KoboldCpp, LM Studio, and Text Generation Web UI.

## Current Limitations
- No support for those drag & drop character cards/images (not planned, I use a very different system)
- 1v1 chat only, no group mode (yet)
- No RAG support for external documents (yet)

## Notes and Requirements
- Runs on Windows 10+ (may work on 7, untested)
- To take full advantage of the LTM and dynamic character systems, a context window of at least 10K is required, 16K+ is *heavily* recommended
- Small models (<=8B params), and models with poor instruction following, may struggle with some of the features
- You can find a list of recommended models in my [Hugging Face Collections](https://huggingface.co/SerialKicked/collections)
- For TTS and Web Search functionalities, KoboldCPP API is required

## License

w(AI)fu.NET is under [CC BY-NC-SA 4.0](https://github.com/SerialKicked/ChatAI/tree/master?tab=License-1-ov-file#readme). You may not use this code for commercial purposes.
