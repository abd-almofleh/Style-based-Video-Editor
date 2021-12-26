from nrclex import NRCLex
path = "./Dataset/data.txt"
data = []

#Getting Highest Emotion
def TopEmo():
    with open(path) as f:
        line = f.readline()
        while line:
            line = f.readline()
            data.append(line)
    for i in range(len(data)-1):
        emotion = NRCLex(data[i])
        print('\n', emotion.sentences)
        print('\n', emotion.top_emotions)
    return 
#TopEmo()

if __name__=='__main__':
    TopEmo()