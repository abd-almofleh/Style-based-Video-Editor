import text2emotion as te
path = r"C:\Users\Noor AB\Desktop\Style-based-Video-Editor\src\sntementAnalysis\Dataset\stdata.txt"
data = []
#getting emotions
def emo():
	with open(path) as f:
		line = f.readline()
		while line:
			line = f.readline()
			data.append(line)
	emotion = te.get_emotion(data[6])
	maxemotion = max(zip(emotion.values(),emotion.keys()))
	v = list(emotion.values())
	k = list(emotion.keys())
	print(emotion)
	print('\n',k[v.index(max(v))],v[v.index(max(v))])
	return

if __name__=='__main__':
	emo()