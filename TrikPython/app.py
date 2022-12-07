import sys
import scipy
import numpy as np
import scipy.cluster.hierarchy as shc
import matplotlib.pyplot as plt
import pandas as pd
import pyodbc
import sklearn 
import seaborn as sns
import plotly.express as px
import io
import os
import urllib
import base64
import json
import plotly
import plotly.graph_objs as go
import plotly.figure_factory as ff
from os.path import join, dirname, realpath
from pandas import read_csv
from pandas.plotting import scatter_matrix
from matplotlib import pyplot
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import train_test_split
from sklearn.model_selection import cross_val_score
from sklearn.model_selection import StratifiedKFold
from sklearn.metrics import classification_report
from sklearn.metrics import confusion_matrix
from sklearn.metrics import accuracy_score
from sklearn.linear_model import LogisticRegression
from sklearn.tree import DecisionTreeClassifier
from sklearn.neighbors import KNeighborsClassifier
from sklearn.discriminant_analysis import LinearDiscriminantAnalysis
from sklearn.naive_bayes import GaussianNB
from sklearn.preprocessing import StandardScaler
from sklearn.decomposition import PCA
from sklearn.cluster import AgglomerativeClustering
from sklearn.svm import SVC
from datetime import datetime
from flask import Flask, render_template, request, redirect, url_for, send_from_directory
app = Flask(__name__)

def translators():
   server1 = 'triksqlserver.database.windows.net'
   database1 = 'TrikDatabase'
   username1 = 'Trikadmin'
   password1 = '{Trikpassword!}'
   cnxn1 = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server1+';DATABASE='+database1+';UID='+username1+';PWD='+ password1)
   cursor = cnxn1.cursor()

   query1 = "SELECT [Name] FROM [dbo].[Categories];"
   sql_query1  = pd.read_sql(query1, cnxn1)
   dfc = pd.DataFrame(sql_query1, columns = ['Name'])
   dfc.rename(columns = {'Name':'Categories'}, inplace = True)
   dfc.index = np.arange(1, len(dfc) + 1)

   query2 = "SELECT [Name] FROM [dbo].[Ages];"
   sql_query2  = pd.read_sql(query2, cnxn1)
   dfa = pd.DataFrame(sql_query2, columns = ['Name'])
   dfa.rename(columns = {'Name':'Ages'}, inplace = True)
   dfa.index = np.arange(1, len(dfa) + 1)

   query3 = "SELECT [Name] FROM [dbo].[Religions];"
   sql_query3  = pd.read_sql(query3, cnxn1)
   dfr = pd.DataFrame(sql_query3, columns = ['Name'])
   dfr.rename(columns = {'Name':'Religions'}, inplace = True)
   dfr.index = np.arange(1, len(dfr) + 1)

   query4 = "SELECT [Name] FROM [dbo].[Genders];"
   sql_query4  = pd.read_sql(query4, cnxn1)
   dfg = pd.DataFrame(sql_query4, columns = ['Name'])
   dfg.rename(columns = {'Name':'Genders'}, inplace = True)
   dfg.index = np.arange(1, len(dfg) + 1)

   query5 = "SELECT [Name] FROM [dbo].[Educations];"
   sql_query5  = pd.read_sql(query5, cnxn1)
   dfe = pd.DataFrame(sql_query5, columns = ['Name'])
   dfe.rename(columns = {'Name':'Educations'}, inplace = True)
   dfe.index = np.arange(1, len(dfe) + 1)

   query6 = "SELECT [Name] FROM [dbo].[Hairs];"
   sql_query6  = pd.read_sql(query6, cnxn1)
   dfh = pd.DataFrame(sql_query6, columns = ['Name'])
   dfh.rename(columns = {'Name':'Hairs'}, inplace = True)
   dfh.index = np.arange(1, len(dfh) + 1)

   query7 = "SELECT [Name] FROM [dbo].[Locations];"
   sql_query7  = pd.read_sql(query7, cnxn1)
   dfl = pd.DataFrame(sql_query7, columns = ['Name'])
   dfl.rename(columns = {'Name':'Locations'}, inplace = True)
   dfl.index = np.arange(1, len(dfl) + 1)

   query8 = "SELECT [Name] FROM [dbo].[Heights];"
   sql_query8  = pd.read_sql(query8, cnxn1)
   dfhe = pd.DataFrame(sql_query8, columns = ['Name'])
   dfhe.rename(columns = {'Name':'Heights'}, inplace = True)
   dfhe.index = np.arange(1, len(dfhe) + 1)

   query9 = "SELECT [Name] FROM [dbo].[Weights];"
   sql_query9  = pd.read_sql(query9, cnxn1)
   dfw = pd.DataFrame(sql_query3, columns = ['Name'])
   dfw.rename(columns = {'Name':'Weights'}, inplace = True)
   dfw.index = np.arange(1, len(dfw) + 1)

   return dfc, dfa, dfr, dfg, dfe, dfh, dfl, dfhe, dfw

#Root URL
@app.route('/')
def index():
   #Database CONNECTION
   server = 'triksqlserver.database.windows.net'
   database = 'TrikDatabase'
   username = 'Trikadmin'
   password = '{Trikpassword!}'
   cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
   cursor = cnxn.cursor()
   query = "SELECT [Code], [CategoryId], [AgeId], [ReligionId], [GenderId], [EducationId], [HairId], [LocationId], [HeightId], [WeightId] FROM [dbo].[Cases];"
   sql_query  = pd.read_sql(query, cnxn)
   df = pd.DataFrame(sql_query, columns = ['CategoryId', 'AgeId','ReligionId','GenderId', 'EducationId', 'HairId', 'LocationId', 'HeightId', 'WeightId'])
   dataset = pd.get_dummies(data=df, columns=['CategoryId','AgeId','ReligionId','GenderId','EducationId','HairId','LocationId','HeightId','WeightId'])
   duplicate = df[df.duplicated(['CategoryId', 'AgeId', 'HeightId', 'WeightId', 'GenderId'])]
   duplicate = duplicate.drop(['ReligionId','EducationId','HairId','LocationId'], axis=1)
   duplicate = duplicate.sort_values(['CategoryId', 'AgeId', 'HeightId'], ascending = [True, True, True])
   #Scatter
   figscatter = px.scatter(df, x="CategoryId", y="LocationId", width=1200, height=600)
   graphJSONtestscatter = json.dumps(figscatter, cls=plotly.utils.PlotlyJSONEncoder)
   #Dendrogram
   figtest = ff.create_dendrogram(df.to_numpy(), orientation='bottom')
   figtest.update_layout(autosize=False, width=1200, height=600)
   graphJSONtest = json.dumps(figtest, cls=plotly.utils.PlotlyJSONEncoder)
   #Machine learning
   pca = PCA(n_components=30)
   pcs = pca.fit_transform(dataset)
   pca.explained_variance_ratio_.cumsum()
   pc1_values = pcs[:,0]
   pc2_values = pcs[:,1]
   X = dataset
   y = dataset
   X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=0)
   sc = StandardScaler()
   X_train = sc.fit_transform(X_train)
   X_test = sc.transform(X_test)
   pca = PCA()
   X_train = pca.fit_transform(X_train)
   X_test = pca.transform(X_test)
   explained_variance = pca.explained_variance_ratio_
   pca = PCA(n_components=1)
   X_train = pca.fit_transform(X_train)
   X_test = pca.transform(X_test)
   classifier = RandomForestClassifier(max_depth=2, random_state=0)
   classifier.fit(X_train, y_train)
   y_pred = classifier.predict(X_test)
   selected_data = dataset.iloc[:, 0:30]
   clusters = shc.linkage(selected_data, method='ward', metric="euclidean")
   #Dendrogram 2 with ML
   dendrogram2data = pd.DataFrame(clusters, columns = ['1', '2','3','4'])
   figtest2 = ff.create_dendrogram(clusters, orientation='bottom')
   figtest2.update_layout(autosize=False, width=1200, height=600)
   graphJSONtest2 = json.dumps(figtest2, cls=plotly.utils.PlotlyJSONEncoder)
   clustering_model = AgglomerativeClustering(n_clusters=10, affinity='euclidean', linkage='ward')
   clustering_model.fit(selected_data)
   array = clustering_model.labels_
   df["Cluster"] = array
   df2 = df
   df2 = df2.sort_values(['Cluster'], ascending = [True])
   dfc, dfa, dfr, dfg, dfe, dfh, dfl, dfhe, dfw = translators()
   #Parallel graph
   figpar = px.parallel_categories(df, dimensions=['CategoryId', 'GenderId', 'EducationId', 'LocationId'], color="CategoryId", color_continuous_scale=px.colors.sequential.Inferno, labels={'CategoryId':'Category', 'GenderId':'Gender', 'EducationId':'Education', 'LocationId':'Location'}, width=1200, height=600)
   graphJSONparallel = json.dumps(figpar, cls=plotly.utils.PlotlyJSONEncoder)
   #3d cube graph
   fig3d = px.scatter_3d(df, x='HeightId', y='WeightId', z='AgeId',color='HairId')
   graphJSON3d = json.dumps(fig3d, cls=plotly.utils.PlotlyJSONEncoder)
   return render_template('index.html', dupl=duplicate.to_html(classes='data', header="true"), data=df.to_html(classes='data', header="true"), clusters=df2.to_html(classes='data', header="true"),
   categories=dfc.to_html(classes='data', header="true"), ages=dfa.to_html(classes='data', header="true"), religions=dfr.to_html(classes='data', header="true"), genders=dfg.to_html(classes='data', header="true"), educations=dfe.to_html(classes='data', header="true"), hairs=dfh.to_html(classes='data', header="true"), locations=dfl.to_html(classes='data', header="true"), heights=dfhe.to_html(classes='data', header="true"), weights=dfw.to_html(classes='data', header="true"),
   plot=graphJSONtest, plot2=graphJSONtest2, plot3=graphJSONtestscatter, plot4=graphJSONparallel, plot5=graphJSON3d)

if __name__ == "__main__":
    from waitress import serve
    serve(app, host="0.0.0.0", port=8080)